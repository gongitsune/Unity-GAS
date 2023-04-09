using NLog;
using TestApp.Abilities;

namespace TestApp.AbilityClasses;

public class AvoidAbility : BaseGamePlayAbility
{
    private static readonly Logger Logger = LogManager.GetLogger("Avoid Ability");
    private readonly float _speed;

    public AvoidAbility(float speed)
    {
        _speed = speed;
    }

    public override BaseGamePlayAbility CloneInstance()
    {
        return new AvoidAbility(_speed);
    }

    protected override void OnActivateAbility()
    {
        Logger.Info("Activate Avoid Ability: Speed -> {0}", _speed);
        EndAbility();
    }

    public override void OnEndAbility()
    {
        Logger.Info("Inactive Avoid Ability: Speed -> {0}", _speed);
    }
}