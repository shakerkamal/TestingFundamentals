namespace TestingFundamentals.UnitTests.Fundamentals;

[TestFixture]
public class StackTests
{
    private TestingFundamentals.Fundamentals.Stack<string> _stack;
    [SetUp]
    public void SetUp()
    {
        _stack = new TestingFundamentals.Fundamentals.Stack<string>();
    }

    [Test]
    public void Push_NullObject_ThrowsArgumentNullException()
    {
        Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);
    }

    [Test]
    public void Push_AnObject_ShouldAddObjectInStack()
    {
        string newString = "Hello";
        _stack.Push(newString);

        Assert.That(_stack.Count, Is.EqualTo(1));
    }

    [Test]
    public void Pop_EmptyStack_ThrowsInvalidOperationException()
    {
        Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
    }

    [Test]
    public void Pop_StackWithFewObjects_ReturnObjectFromTop()
    {
        _stack.Push("1");
        _stack.Push("2");
        _stack.Push("3");

        var response = _stack.Pop();

        Assert.That(response, Is.EqualTo("3"));
    }

    [Test]
    public void Pop_StackWithFewObjects_RemoveObjectFromTop()
    {
        _stack.Push("1");
        _stack.Push("2");
        _stack.Push("3");

        var response = _stack.Pop();

        Assert.That(_stack.Count, Is.EqualTo(2));
    }

    [Test]
    public void Peek_EmptyStack_ThrowsInvalidOperationException()
    {
        Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
    }

    [Test]
    public void Peek_StackWithFewObjects_ReturnObjectFromTop()
    {
        _stack.Push("1");
        _stack.Push("2");
        _stack.Push("3");

        var response = _stack.Peek();

        Assert.That(response, Is.EqualTo("3"));
    }

    [Test]
    public void Peek_StackWithFewObjects_DoesNotRemoveObjectFromTop()
    {
        _stack.Push("1");
        _stack.Push("2");
        _stack.Push("3");

        var response = _stack.Peek();

        Assert.That(_stack.Count, Is.EqualTo(3));
    }
}
