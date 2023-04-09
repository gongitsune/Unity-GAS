using System.Text;

namespace TestApp.Abilities;

public readonly struct TagContainer : IEquatable<TagContainer>
{
    private readonly string[] _separateTags;

    public override string ToString()
    {
        var builder = new StringBuilder();
        // ReSharper disable once ForCanBeConvertedToForeach
        for (var i = 0; i < _separateTags.Length; i++)
        {
            builder.Append(_separateTags[i]);
            builder.Append('.');
        }

        return builder.ToString();
    }

    private TagContainer(string[] separateTags)
    {
        _separateTags = separateTags;
    }

    public bool IsInitialized => _separateTags != default;

    /// <summary>
    ///     文字列からタグを生成
    /// </summary>
    /// <param name="tagText"></param>
    /// <returns></returns>
    public static TagContainer BuildTagContainerByString(in string tagText)
    {
        if (string.IsNullOrEmpty(tagText)) throw new ArgumentException("\"tagText\" should not be empty!");
        return new TagContainer(tagText.Split('.').ToArray());
    }

    public bool NearEqual(TagContainer other)
    {
        for (var i = 0; i < Math.Min(_separateTags.Length, other._separateTags.Length); i++)
            if (_separateTags[i] != other._separateTags[i])
                return false;

        return true;
    }


    public bool Equals(TagContainer other)
    {
        if (_separateTags == null) return false;
        if (_separateTags.Length != other._separateTags.Length) return false;
        // ReSharper disable once LoopCanBeConvertedToQuery

        for (var i = 0; i < _separateTags.Length; i++)
            if (_separateTags[i] != other._separateTags[i])
                return false;

        return true;
    }

    public override bool Equals(object? obj)
    {
        return obj is TagContainer other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _separateTags.GetHashCode();
    }

    public static bool operator ==(TagContainer left, TagContainer right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(TagContainer left, TagContainer right)
    {
        return !(left == right);
    }
}