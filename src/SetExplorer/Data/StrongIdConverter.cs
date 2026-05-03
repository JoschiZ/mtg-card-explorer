using SetExplorer.Client.Core;
using SetExplorer.Client.Core.Cards;
using SetExplorer.Client.Core.Collections;
using SetExplorer.Client.Core.Explorations;
using Vogen;

namespace SetExplorer.Data;

[EfCoreConverter<ScryfallCardId>]
[EfCoreConverter<CollectionId>]
[EfCoreConverter<ExplorationId>]
[EfCoreConverter<UserId>]
public abstract partial class VogenEfCoreConverter;