namespace RenjuTests;

[TestFixture]
public class SessionTests
{
    private ISession _singleSession;
    private IMessageService _messageService;
    private IPlayer _blackPlayer, _whitePlayer, _blackPlayerWithReturn;

    [SetUp]
    public void Init()
    {
        _blackPlayer = Mock.Of<Player>();
        _whitePlayer = Mock.Of<Player>();
        _blackPlayerWithReturn = Mock.Of<Player>();
        _messageService = Mock.Of<IMessageService>();

        Mock.Get(_blackPlayer)
            .Setup(x => x.MakeMove(It.IsAny<CancellationToken>()))
            .Returns(
                Task.Delay(Timeout.Infinite)
                    .ContinueWith(_ => new Move(0, 0, CellStone.Black))
            );
        Mock.Get(_whitePlayer)
            .Setup(x => x.MakeMove(It.IsAny<CancellationToken>()))
            .Returns(
                Task.Delay(Timeout.Infinite)
                    .ContinueWith(_ => new Move(0, 0, CellStone.White))
            );
        Mock.Get(_blackPlayerWithReturn)
            .Setup(x => x.MakeMove(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Move(0, 0, CellStone.Black));

        Mock<IMessageService> messageService = Mock.Get(_messageService);
        messageService.Setup(x => x.ShowAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            ))
            .ReturnsAsync(true);
        messageService.Setup(x => x.ShowAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            ))
            .Returns(Task.CompletedTask);

        _singleSession = new SingleSession(_blackPlayer, _whitePlayer);
    }

    [TearDown]
    public async Task Cleanup()
    {
        try
        {
            await _singleSession.Terminate();
        }
        catch (InvalidOperationException)
        {
            // Ignore
        }
        finally
        {
            _singleSession = null!;
        }
    }

    [Test]
    public void ShouldNotHaveStartedBeforeStart()
    {
        Assert.That(_singleSession.Result, Is.EqualTo(GameResult.NotStarted));
    }

    [Test]
    public void ShouldHaveStartedAfterStart()
    {
        _singleSession.Play();
        Assert.That(_singleSession.Result, Is.EqualTo(GameResult.OnGoing));
    }

    [Test]
    public async Task ShouldHaveEndedAfterEnd()
    {
        _ = _singleSession.Play();
        await _singleSession.Terminate();
        Assert.That(_singleSession.Result, Is.EqualTo(GameResult.Cancelled));
    }

    [Test]
    public void ShouldNotStartTwice()
    {
        _singleSession.Play();
        Assert.ThrowsAsync<InvalidOperationException>(
            () => _singleSession.Play()
        );
    }

    [Test]
    public async Task ShouldNotTerminateTwice()
    {
        _ = _singleSession.Play();
        await _singleSession.Terminate();

        Assert.ThrowsAsync<InvalidOperationException>(
            async () => await _singleSession.Terminate()
        );
    }

    [Test]
    public void BlackPlayerIsBlack()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_singleSession.BlackPlayer.IsBlack, Is.True);
            Assert.That(_singleSession.BlackPlayer.IsWhite, Is.False);
        });
    }

    [Test]
    public void WhitePlayerIsWhite()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_singleSession.WhitePlayer.IsBlack, Is.False);
            Assert.That(_singleSession.WhitePlayer.IsWhite, Is.True);
        });
    }

    [Test]
    public void ShouldHaveAtLeastOneRound()
    {
        Assert.That(_singleSession.Rounds.LongCount(), Is.GreaterThan(0L));
    }

    [Test]
    public void ShouldHaveCurrentRound()
    {
        Assert.That(_singleSession.CurrentRound, Is.Not.Null);
    }

    [Test]
    public void ShouldHaveCurrentBoard()
    {
        Assert.That(_singleSession.CurrentBoard, Is.Not.Null);
    }

    [Test]
    public async Task ShouldHaveCurrentBoardChangedOnChange()
    {
        SingleSession session = new(_blackPlayerWithReturn, _whitePlayer);
        bool changed = false;
        session.BoardChanged += () => changed = true;

        _ = session.Play();

        Assert.That(changed, Is.True);

        await session.Terminate();
    }

    [Test]
    public async Task ShouldNotChangeOnBoardChangedUnsubscribe()
    {
        SingleSession session = new(_blackPlayerWithReturn, _whitePlayer);
        bool changed = false;
        Action func = () => changed = true;

        session.BoardChanged += func;
        session.BoardChanged -= func;

        _ = session.Play();

        Assert.That(changed, Is.False);

        await session.Terminate();
    }

    [Test]
    public void ShouldHaveGameEndedEvent()
    {
        Action action = Session.AlertOnGameEnded(_singleSession, _messageService);
        Assert.DoesNotThrow(() => action());
    }
    
    [Test]
    public async Task ShouldStartTerminatedClone()
    {
        _ = _singleSession.Play();
        await _singleSession.Terminate();
        
        ISession clone = (ISession)(_singleSession as ICloneable).Clone();
        
        Assert.DoesNotThrow(() => clone.Play());
        Assert.DoesNotThrowAsync(() => clone.Terminate());
    }
}
