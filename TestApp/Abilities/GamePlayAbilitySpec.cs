namespace TestApp.Abilities;

public readonly struct GamePlayAbilitySpec
{
    public readonly ITagCombine ActivationBlockedTags;
    public readonly ITagCombine OwnersTags;
    public readonly AbilitySystem Owner;
    public readonly TagContainer ActivateTag;

    public GamePlayAbilitySpec(AbilitySystem owner, ITagCombine activationBlockedTags, ITagCombine ownersTags,
        TagContainer activateTag = default)
    {
        Owner = owner;
        ActivationBlockedTags = activationBlockedTags;
        OwnersTags = ownersTags;
        ActivateTag = activateTag;
    }
}