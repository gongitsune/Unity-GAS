namespace TestApp.Abilities;

/// <summary>
///     アビリティのデータ
/// </summary>
public readonly struct GamePlayAbilityData
{
    public readonly AbilityTagSetting TagSetting;
    private readonly BaseGamePlayAbility _abilityClass;

    public GamePlayAbilityData(AbilityTagSetting tagSetting, BaseGamePlayAbility abilityClass)
    {
        TagSetting = tagSetting;
        _abilityClass = abilityClass;
    }

    public bool TryActivateAbility(GamePlayAbilitySpec spec, out BaseGamePlayAbility? abilityInstance)
    {
        if (!CanActivateAbility(spec))
        {
            abilityInstance = default;
            return false;
        }

        abilityInstance = _abilityClass.CloneInstance();
        abilityInstance.ActivateAbility(spec, this);
        return true;
    }

    /// <summary>
    ///     アビリティを起動できるか
    /// </summary>
    /// <returns></returns>
    private bool CanActivateAbility(GamePlayAbilitySpec spec)
    {
        if (TagSetting.AbilityTags != null)
        {
            if (spec.ActivateTag.IsInitialized && !TagSetting.AbilityTags.HasTag(spec.ActivateTag)) return false;
            if (spec.ActivationBlockedTags.HasEvenOneTag(TagSetting.AbilityTags)) return false;
        }

        if (TagSetting.ActivationRequiredTags != null &&
            TagSetting.ActivationRequiredTags.Count() != 0 &&
            !spec.OwnersTags.HasAllOtherTag(TagSetting.ActivationRequiredTags)
           ) return false;
        if (TagSetting.ActivationBlockedTags != null &&
            spec.OwnersTags.HasEvenOneTag(TagSetting.ActivationBlockedTags)
           ) return false;

        return true;
    }

    /// <summary>
    ///     起動中のアビリティをキャンセルする必要があるか
    /// </summary>
    /// <param name="spec"></param>
    /// <returns></returns>
    private bool IsNeedToCancel(GamePlayAbilitySpec spec)
    {
        if (TagSetting.ActivationRequiredTags != null &&
            TagSetting.ActivationRequiredTags.Count() != 0 &&
            !spec.OwnersTags.HasAllOtherTag(TagSetting.ActivationRequiredTags)
           ) return true;
        if (TagSetting.ActivationBlockedTags != null &&
            spec.OwnersTags.HasEvenOneTag(TagSetting.ActivationBlockedTags)
           ) return true;
        return false;
    }
}