using SetExplorer.Client.Core;
using SetExplorer.Client.Features.Cards;
using SetExplorer.Client.Features.Explorations;
using Vogen;
using CollectionId = SetExplorer.Client.Features.Collections.CollectionId;



namespace SetExplorer.Data;

[EfCoreConverter<ScryfallCardId>]
[EfCoreConverter<CollectionId>]
[EfCoreConverter<ExplorationId>]
[EfCoreConverter<UserId>]
public abstract partial class VogenEfCoreConverter;