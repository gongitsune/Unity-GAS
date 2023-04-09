// ReSharper disable ForCanBeConvertedToForeach


// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator

namespace TestApp.Abilities;

public class AbilitySystem
{
    private readonly Dictionary<GamePlayAbilityData, List<BaseGamePlayAbility>> _abilityDataList = new();
    private readonly MutableTagCombine _activationBlockTags = new();
    private readonly MutableTagCombine _ownersTags = new();

    public void GiveAbility(GamePlayAbilityData abilityData)
    {
        if (!_abilityDataList.ContainsKey(abilityData)) _abilityDataList[abilityData] = new List<BaseGamePlayAbility>();
    }


    public bool TryActivateAbility(GamePlayAbilityData abilityData)
    {
        if (!_abilityDataList.ContainsKey(abilityData)) return false;

        if (!abilityData.TryActivateAbility(new GamePlayAbilitySpec(this, _activationBlockTags, _ownersTags),
                out var abilityInstance)) return false;
        _abilityDataList[abilityData].Add(abilityInstance!);

        // ブロックするタグを追加
        var tags = abilityData.TagSetting.BlockAbilitiesWithTag?.GetTags();
        if (tags is not null) _activationBlockTags.AddRangeTags(tags);

        // 所持者のタグを追加
        if (abilityData.TagSetting.ActivationOwnedTags is { } ownedTags) _ownersTags.AddRangeTags(ownedTags.GetTags());

        // アビリティをキャンセル
        var cancelTags = abilityData.TagSetting.CancelAbilitiesWithTag;
        if (cancelTags is null) return true;

        foreach (var key in _abilityDataList.Keys)
            if (key.TagSetting.AbilityTags?.HasEvenOneTag(cancelTags) ?? false)
                TryCancelAbility(key);

        return true;
    }

    public bool TryCancelAbility(GamePlayAbilityData abilityData)
    {
        if (!_abilityDataList.TryGetValue(abilityData, out var abilityList)) return false;

        for (var i = 0; i < abilityList.Count; i++) abilityList[i].OnEndAbility();
        abilityList.Clear();
        if (abilityData.TagSetting.ActivationOwnedTags?.GetTags() is { } ownedTags)
            _ownersTags.RemoveAllTags(v => ownedTags.Contains(v));

        return true;
    }

    public void EndAbilityInstance(GamePlayAbilityData data, BaseGamePlayAbility abilityInstance)
    {
        if (!_abilityDataList.TryGetValue(data, out var abilityList)) return;

        abilityInstance.OnEndAbility();
        abilityList.Remove(abilityInstance);

        if (abilityList.Count != 0) return;

        if (data.TagSetting.BlockAbilitiesWithTag?.GetTags() is { } blockedTags)
            _activationBlockTags.RemoveAllTags(v => blockedTags.Contains(v));
        if (data.TagSetting.ActivationOwnedTags?.GetTags() is { } ownedTags)
            _ownersTags.RemoveAllTags(v => ownedTags.Contains(v));
    }
}