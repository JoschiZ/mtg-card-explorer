using CardExplorer.Client.Core;
using CardExplorer.Client.Features.Cards;
using CardExplorer.Client.Features.Explorations;
using Vogen;
using CollectionId = CardExplorer.Client.Features.Collections.CollectionId;



namespace CardExplorer.Data;

[EfCoreConverter<ScryfallCardId>]
[EfCoreConverter<CollectionId>]
[EfCoreConverter<ExplorationId>]
[EfCoreConverter<UserId>]
public abstract partial class VogenEfCoreConverter;