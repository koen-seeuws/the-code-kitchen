# The Code Kitchen
The Code Kitchen is a project created for the Involved Code Retreat of september 2025 in which I've been playing around with .NET Orleans, Azure Container Apps, Azure SignalR Service and Azure Event Hubs.

## Gameplay
In the game, cooks (players) are divided into multiple kitchens (teams). Each kitchen in a game receives identical orders in which dishes are requested to be prepared. In order to prepare a dish, the cooks need to: read recipes, take the correct ingredients, prepare them using the correct equipment for the correct amount of time and then mix them together. When a dish is ready, it needs to be delivered to the order and in case everything was delivered, the order needs to be completed. Cooks can only hold 1 piece of food at a time. 

Cooks can set timers for which they will later on receive an event when the timers elapses. Cooks can also communicate with each other by sending messages.

The kitchens are rated by how complete the order is (no lissing items?), how well they followed all the required steps for a recipe and whether they delivered the dished in time.
