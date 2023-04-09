namespace TestApp.Abilities;

public readonly struct AbilityTagSetting
{
    /// <summary>
    ///     アビリティのタグ
    /// </summary>
    public ITagCombine? AbilityTags { get; init; }

    /// <summary>
    ///     所持者が指定されたタグを持っているならアビリティをブロック
    /// </summary>
    public ITagCombine? ActivationBlockedTags { get; init; }

    /// <summary>
    ///     実行されたときに指定されたタグを所持者に付与
    /// </summary>
    public ITagCombine? ActivationOwnedTags { get; init; }

    /// <summary>
    ///     所持者が指定されたタグを持っていないなら実行できない
    /// </summary>
    public ITagCombine? ActivationRequiredTags { get; init; }

    /// <summary>
    ///     実行中指定されたタグを持っているアビリティをブロック
    /// </summary>
    public ITagCombine? BlockAbilitiesWithTag { get; init; }

    /// <summary>
    ///     実行時指定されたタグを持っているアビリティをキャンセル
    /// </summary>
    public ITagCombine? CancelAbilitiesWithTag { get; init; }

    public AbilityTagSetting(
        in ITagCombine abilityTags,
        in ITagCombine cancelAbilitiesWithTag,
        in ITagCombine blockAbilitiesWithTag,
        in ITagCombine activationOwnedTags,
        in ITagCombine activationRequiredTags,
        in ITagCombine activationBlockedTags)
    {
        AbilityTags = abilityTags;
        CancelAbilitiesWithTag = cancelAbilitiesWithTag;
        BlockAbilitiesWithTag = blockAbilitiesWithTag;
        ActivationOwnedTags = activationOwnedTags;
        ActivationRequiredTags = activationRequiredTags;
        ActivationBlockedTags = activationBlockedTags;
    }
}