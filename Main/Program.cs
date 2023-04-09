using System.Reflection;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using TestApp.Abilities;
using TestApp.AbilityClasses;

#region Log Settings

var logConfig = new LoggingConfiguration();
var logConsole = new ColoredConsoleTarget("logConsole")
{
    Layout = new SimpleLayout("[${longdate}] [${level:uppercase=true}] [${logger}] ${message}")
};
logConfig.AddRule(LogLevel.Info, LogLevel.Fatal, logConsole);
LogManager.Configuration = logConfig;

#endregion


var abilitySystem = new AbilitySystem();

var attackAbility = new GamePlayAbilityData(new AbilityTagSetting
{
    AbilityTags = new ImmutableTagCombine(new[] { TagContainer.BuildTagContainerByString("Ability.Attack.Combo1") }),
    BlockAbilitiesWithTag = new ImmutableTagCombine(new[] { TagContainer.BuildTagContainerByString("Ability.Attack") }),
    ActivationOwnedTags = new ImmutableTagCombine(new[] { TagContainer.BuildTagContainerByString("Ability.Attacking") }),
    // ActivationBlockedTags = new ImmutableTagCombine(new[] { TagContainer.BuildTagContainerByString("Ability.Attacking") })
}, new AttackAbility(10));
var avoidAbility = new GamePlayAbilityData(new AbilityTagSetting
{
    AbilityTags = new ImmutableTagCombine(new[] { TagContainer.BuildTagContainerByString("Ability.Avoid") }),
    BlockAbilitiesWithTag = new ImmutableTagCombine(new[] { TagContainer.BuildTagContainerByString("Ability") }),
    // CancelAbilitiesWithTag = new ImmutableTagCombine(new[] { TagContainer.BuildTagContainerByString("Ability.Attack") })
}, new AvoidAbility(40));


void SetUp()
{
    abilitySystem.GiveAbility(attackAbility);
    abilitySystem.GiveAbility(avoidAbility);
}

async void AbilityTest()
{
    abilitySystem.TryActivateAbility(attackAbility);
    await Task.Delay(TimeSpan.FromSeconds(0.5f));
    abilitySystem.TryActivateAbility(attackAbility);
    abilitySystem.TryActivateAbility(avoidAbility);
}

SetUp();
AbilityTest();
await Task.Delay(TimeSpan.FromSeconds(2));

Console.WriteLine(typeof(AbilitySystem)
    .GetField("_ownersTags", BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance)
    ?.GetValue(abilitySystem)?.ToString() ?? "Null");