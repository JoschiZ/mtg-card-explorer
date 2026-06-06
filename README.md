# Card Explorer
[Card-Explorer](https://card-explorer.joschiz.pro) is a tool that allows you to explore magic cards at your own pace.
Mainly it will remember the card you have already seen in a given "exploration", which enables you to leave and come back later without having to remember where you left off.
You can also save interesting cards to "collections", which can be exported directly to your deckbuilding site of choice, like [Archidekt](https://archidekt.com) or [Moxfield](https://moxfield.com).
A "collection" could be a deck you are currently searching cards for, or just cards that interest you in one way or another.

Card Explorer does not aim to be a deckbuilding tool, but to augment your building experience.

### Setup & Run locally
1. **Start the Database:**
   ```bash
   cd docker/test-db
   docker-compose up -d
   ```
2. **Launch the App:**
   From the project root:
   ```bash
   dotnet run --project src/SetExplorer
   ```
3. **Explore:**
   Open [https://localhost:5001](https://localhost:5001) in your favorite browser.
