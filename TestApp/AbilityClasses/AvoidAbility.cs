using NLog;
using TestApp.Abilities;

namespace TestApp.AbilityClasses;

public class AvoidAbility : BaseGamePlayAbility
{
    private readonly float _speed;
    private static readonly Logger Logger = LogManager.GetLogger("Avoid Ability");

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
        Logger.Info("Activate Avoid Ability: {0}", _speed);
        EndAbility();
    }

    public override void OnEndAbility()
    {
        Logger.Info("Inactive Avoid Ability: {0}", _speed);
    }
}