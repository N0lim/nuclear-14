using Content.Shared.Nutrition.EntitySystems;
using Content.Shared.Storage;
using Robust.Shared.Random;
using Content.Shared.Coordinates;

namespace Content.Shared._NC.SpawnWhenOpened;

public sealed partial class SpawnWhenOpenedSystem : EntitySystem
{
    [Dependency] private readonly IRobustRandom _random = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<SpawnWhenOpenedComponent, OpenableOpenedEvent>(OnOpened);
    }

    private void OnOpened(EntityUid uid, SpawnWhenOpenedComponent comp, ref OpenableOpenedEvent args)
    {
        if (!comp.IsRepeatable && comp.IsAlreadyOpened)
            return;

        comp.IsAlreadyOpened = true;
        foreach (var ent in EntitySpawnCollection.GetSpawns(comp.Prototypes, _random))
        {
            Spawn(ent, uid.ToCoordinates());
        }
    }
}
