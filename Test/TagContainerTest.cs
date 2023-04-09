using TestApp.Abilities;

namespace TestProjectTest;

public class TagContainerTests
{
    private ITagCombine _tagCombine = null!;

    [SetUp]
    public void Setup()
    {
        _tagCombine = new ImmutableTagCombine(new[]
        {
            TagContainer.BuildTagContainerByString("Ability.Attack"),
            TagContainer.BuildTagContainerByString("Ability.Attack.Combo2")
        });
    }

    [TestCase("Ability.Attack", true)]
    [TestCase("Ability.Attack.Combo2", false)]
    [TestCase("Ability.Defense", false)]
    public void TagContainerNearEqualTest(string otherTag, bool exact)
    {
        var container1 = TagContainer.BuildTagContainerByString("Ability.Attack.Combo1");
        Assert.That(container1.NearEqual(TagContainer.BuildTagContainerByString(otherTag)), Is.EqualTo(exact));
    }

    [TestCase("Ability.Attack", true)]
    [TestCase("Ability.Defense", false)]
    public void TagCombineHasTagTest(string tagText, bool exact)
    {
        Assert.That(_tagCombine.HasTag(TagContainer.BuildTagContainerByString(tagText)), Is.EqualTo(exact));
    }

    [TestCase(new[] { "Ability.Attack", "Ability" }, true)]
    [TestCase(new[] { "Ability.Defense", "Ability" }, false)]
    public void TagCombineHasAllOtherTagTest(string[] tagTextList, bool exact)
    {
        Assert.That(
            _tagCombine.HasAllOtherTag(
                new ImmutableTagCombine(tagTextList.Select(v => TagContainer.BuildTagContainerByString(v)).ToArray())),
            Is.EqualTo(exact));
    }
    
    [TestCase(new[] { "Ability.Attack", "Ability" }, true)]
    [TestCase(new[] { "Ability.Defense", "Ability" }, true)]
    [TestCase(new[] { "Ability.Defense", "Ability.Avoid" }, false)]
    public void TagCombineHasEvenOneTagTest(string[] tagTextList, bool exact)
    {
        Assert.That(
            _tagCombine.HasEvenOneTag(
                new ImmutableTagCombine(tagTextList.Select(v => TagContainer.BuildTagContainerByString(v)).ToArray())),
            Is.EqualTo(exact));
    }
}