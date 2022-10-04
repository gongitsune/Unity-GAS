// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable ForCanBeConvertedToForeach

// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator

using System.Text;

namespace TestApp.Abilities;

public interface ITagCombine
{
    /// <summary>
    ///     指定されたタグを持っているか
    /// </summary>
    /// <param name="other">対象</param>
    /// <returns></returns>
    bool HasTag(TagContainer other)
    {
        for (var i = 0; i < GetTags().Count; i++)
            if (GetTags()[i].NearEqual(other))
                return true;

        return false;
    }

    /// <summary>
    ///     比較対象のタグをすべて持っているか
    /// </summary>
    /// <returns></returns>
    bool HasAllOtherTag<T>(T other) where T : ITagCombine
    {
        var otherTags = other.GetTags();
        for (var i = 0; i < otherTags.Count; i++)
            if (!HasTag(otherTags[i]))
                return false;

        return true;
    }

    /// <summary>
    ///     比較対象のタグを一つでも持っているか
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    bool HasEvenOneTag<T>(T other) where T : ITagCombine
    {
        for (var i = 0; i < GetTags().Count; i++)
            if (other.HasTag(GetTags()[i]))
                return true;

        return false;
    }

    IReadOnlyList<TagContainer> GetTags();
    int Count();
}

/// <summary>
///     複数のタグをまとめて管理
/// </summary>
public class ImmutableTagCombine : ITagCombine
{
    private readonly TagContainer[] _tags;

    public ImmutableTagCombine(TagContainer[] tags)
    {
        _tags = tags;
    }

    public IReadOnlyList<TagContainer> GetTags()
    {
        return _tags;
    }

    public int Count()
    {
        return _tags.Length;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append('[');
        for (var i = 0; i < _tags.Length; i++)
        {
            builder.Append(_tags[i].ToString());
            builder.Append(", ");
        }

        builder.Append(']');

        return builder.ToString();
    }
}

public class MutableTagCombine : ITagCombine
{
    private readonly List<TagContainer> _tags;

    public MutableTagCombine(List<TagContainer> tags)
    {
        _tags = tags;
    }

    public MutableTagCombine()
    {
        _tags = new List<TagContainer>();
    }


    public IReadOnlyList<TagContainer> GetTags()
    {
        return _tags;
    }

    public int Count()
    {
        return _tags.Count;
    }

    public void AddTag(TagContainer tag)
    {
        _tags.Add(tag);
    }

    public void RemoveTag(TagContainer tag)
    {
        _tags.Remove(tag);
    }

    public void AddRangeTags(IEnumerable<TagContainer> tags)
    {
        _tags.AddRange(tags);
    }

    public void RemoveAllTags(Predicate<TagContainer> match)
    {
        _tags.RemoveAll(match);
    }
    
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append('[');
        for (var i = 0; i < _tags.Count; i++)
        {
            builder.Append(_tags[i].ToString());
            builder.Append(", ");
        }

        builder.Append(']');

        return builder.ToString();
    }
}