namespace RenjuTests;

[TestFixture]
public class PlayerTests
{
    private IPlayer _blackAbstract, _whiteHuman;

    [SetUp]
    public void Init()
    {
        _blackAbstract = Mock.Of<Player>(player =>
            player.Color == CellStone.Black
        );
        _whiteHuman = Mock.Of<HumanPlayer>(player =>
            player.Color == CellStone.White
        );

        _blackAbstract.Name = "Black Player";
        _whiteHuman.Name = "White Player";

        Mock.Get(_blackAbstract)
            .Setup(x => x.MakeMove(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Move(0, 0, CellStone.Black));
        Mock.Get(_whiteHuman)
            .Setup(x => x.MakeMove(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Move(0, 0, CellStone.White));
    }

    [Test]
    public void ShouldHaveColor()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_blackAbstract.Color, Is.Not.EqualTo(CellStone.Empty));
            Assert.That(_whiteHuman.Color, Is.Not.EqualTo(CellStone.Empty));
        });
    }

    [Test]
    public void ShouldMakeMove()
    {
        Assert.Multiple(async () =>
        {
            Move move = await _blackAbstract.MakeMove();
            Assert.That(move.Stone, Is.EqualTo(CellStone.Black));
        });
    }

    [Test]
    public void ShouldHaveName()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_blackAbstract.Name, Is.EqualTo("Black Player"));
            Assert.That(_whiteHuman.Name, Is.EqualTo("White Player"));
        });
    }

    [Test]
    public async Task HumanShouldExecuteAwaitEvent()
    {
        // Arrange
        if (_whiteHuman is not HumanPlayer humanPlayer)
        {
            Assert.Fail("The player is not a human player");
            return;
        }

        humanPlayer.AwaitMove += _ =>
            Task.FromResult(new Move(0, 0, CellStone.White));

        // Act
        Move move = await humanPlayer.MakeMove();
        // Assert
        Assert.That(move.Stone, Is.EqualTo(CellStone.White));
    }

    [Test]
    public void HumanShouldThrowOnUnsubscribedEvent()
    {
        HumanPlayer humanPlayer = new();
        Func<CancellationToken, Task<Move>> func = _ =>
            Task.FromResult(new Move(0, 0, CellStone.White));

        humanPlayer.AwaitMove += func;
        humanPlayer.AwaitMove -= func;

        Assert.ThrowsAsync<InvalidOperationException>(
            async () => await humanPlayer.MakeMove()
        );
    }

    [Test]
    public void HumanShouldThrowOnClearedEvent()
    {
        HumanPlayer humanPlayer = new();

        humanPlayer.AwaitMove += _ =>
            Task.FromResult(new Move(0, 0, CellStone.White));
        humanPlayer.ClearSubscriptions();

        Assert.ThrowsAsync<InvalidOperationException>(
            async () => await humanPlayer.MakeMove()
        );
    }

    [Test]
    public void HumanShouldThrowOnNullEvent()
    {
        HumanPlayer humanPlayer = new();
        Assert.Multiple(() =>
        {
            Assert.Throws<ArgumentNullException>(
                () => humanPlayer.AwaitMove += null!
            );
            Assert.Throws<ArgumentNullException>(
                () => humanPlayer.AwaitMove -= null!
            );
        });
    }

    [Test]
    public async Task BotShouldMakeMove()
    {
        // Arrange
        // TODO: Replace with SimpleBotPlayer when implemented
        SimpleBotPlayer bot = Mock.Of<SimpleBotPlayer>(player =>
            player.Color == CellStone.Black
        );
        Mock.Get(bot)
            .Setup(x => x.MakeMove(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Move(0, 0, CellStone.Black));
        
        Move move = await bot.MakeMove();
        
        Assert.That(move.Stone, Is.EqualTo(CellStone.Black));
    }
}
