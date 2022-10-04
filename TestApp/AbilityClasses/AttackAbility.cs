using NLog;
using TestApp.Abilities;

namespace TestApp.AbilityClasses;

public class AttackAbility : BaseGamePlayAbility
{
    private static readonly Logger Logger = LogManager.GetLogger("Attack Ability");
    private readonly float _power;
    private readonly CancellationTokenSource _token = new();

    public AttackAbility(float power)
    {
        _power = power;
    }

    protected override async void OnActivateAbility()
    {
        Logger.Info($"Activate Attack Ability: {_power}");
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(1), _token.Token);
            EndAbility();
        }
        catch (Exception)
        {
            // ignored
        }
    }

    public override void OnEndAbility()
    {
        Logger.Info("End Attack ability: {0}", _power);
        _token.Cancel();
    }

    public override BaseGamePlayAbility CloneInstance()
    {
        return new AttackAbility(_power);
    }
}