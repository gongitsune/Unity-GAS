namespace TestApp.Abilities;

/// <summary>
///     アビリティの実行をするクラス
/// </summary>
public abstract class BaseGamePlayAbility
{
    protected GamePlayAbilitySpec Spec;
    protected GamePlayAbilityData AbilityData;
    public virtual BaseGamePlayAbility CloneInstance()
    {
        return MemberwiseClone() as BaseGamePlayAbility ?? throw new Exception("Instance could not clone.");
    }
    
    public void ActivateAbility(GamePlayAbilitySpec spec, GamePlayAbilityData abilityData)
    {
        Spec = spec;
        AbilityData = abilityData;
        
        OnActivateAbility();
    }
    
    protected void EndAbility()
    {
        Spec.Owner.EndAbilityInstance(AbilityData, this);
    }

    protected abstract void OnActivateAbility();

    public abstract void OnEndAbility();
}