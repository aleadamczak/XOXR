### Introduction

I'm Aleksandra, and this was my first game development project. Since it was my first time using Unity, I decided to take on the responsibility of creating the UI. This way, I could familiarize myself with the available game objects and get accustomed to Unity's interface. Manipulating 3D objects seemed more complex, which is why Rosa and Laura, who are more experienced, handled these elements of the game.

I started with the CanvasMain, which served as the general environment for our game. The user interface included all the tools needed to interact with the animals, four buttons at the bottom, and an indication of how many coins the player had earned so far. The entire interface was inspired by *Minecraft*, so we used pixelated versions of all images and fonts. We tried to use as many as we could find online, but whenever something was missing, Lara stepped in to create the icons herself :) I integrated these icons and created buttons by merging multiple PNG images or making slight adjustments to the ones I found:

![image](https://github.com/user-attachments/assets/07b06cde-0afa-46be-b5e7-9ccb31a3f241)
![image](https://github.com/user-attachments/assets/a5db039c-e490-4348-8c2a-4c3a65f70607)
![image](https://github.com/user-attachments/assets/32049bd3-7774-4542-9c73-ccd4f6a31b94)

![image](https://github.com/user-attachments/assets/e93798bb-46c5-4797-839a-4ae818fc535b)
![image](https://github.com/user-attachments/assets/c0fbf569-c84a-4e86-bb39-2dd37a3d0e8d)

The buttons also had audio effects assigned, but later we changed the trigger for the audio to play when the player clicks on the animal after selecting one of the buttons. When selected, the buttons also change to highlighted images:

![Unity_vNz8K2G2Zr](https://github.com/user-attachments/assets/1602e56b-0c2f-4cee-85ec-2322a18ee947)

In the top-right corner, the user can open a shop with other farm animals available for sale. If the user has enough coins, they can add the animal to the cart.

![Unity_CQIrtFgn3O](https://github.com/user-attachments/assets/0456fc3f-f2d8-4b58-8910-bdd81e156708)

The shopping cart panel allows the user to delete any unwanted items and see the current total for all items placed in the basket.

![Unity_UXeZxeZyQm](https://github.com/user-attachments/assets/a7e10309-d1c6-4178-afa6-272471bd48e1)

Once the items are purchased, the player is transferred to another canvas where they can place the new little farm inhabitants :) After they're placed, the player is brought back to the main view.

![Unity_n6lR3tMTYY](https://github.com/user-attachments/assets/f727c4a1-c4c1-430a-acf2-650f08a941fb)

---

### Code Implementation

The shop actions are handled by the Shop Manager:

![image](https://github.com/user-attachments/assets/750e8d54-1b8c-40e1-aee3-d7fa9627b4ab)

Due to the many actions handled within the ShopManager script, it has grown to quite a large extent over time. It handles enabling and disabling buttons, calculating and updating quantities and prices. It also includes a placeholder for items that are eventually purchased, which are then passed to the script handling animal placement, `SceneInitializer`.

![image](https://github.com/user-attachments/assets/430afbf2-068d-4978-ad6a-e0897c7eedd6)
![image](https://github.com/user-attachments/assets/69975e71-30ef-4f8a-8c93-b0410fd11c98)

When the game starts, listeners are added to the buttons to handle actions on click, and all values are reset:

![image](https://github.com/user-attachments/assets/abe31a65-1d81-4a31-9997-6d2672c22104)

All values are transformed into TextMeshPro, and the basket acts as a placeholder that will be passed to the `SceneInitializer`:

![image](https://github.com/user-attachments/assets/194c5c09-7859-40da-95a1-50fbc188aec7)

We also ensure that the player has enough coins to place certain items in the basket:

![image](https://github.com/user-attachments/assets/81dd3790-f8bc-42f3-a044-001bf10e9bf4)

There are two methods for handling actions related to adding and deleting items from the basket:

![image](https://github.com/user-attachments/assets/adfb3d71-360e-46ff-ab22-34aec7f18391)
![image](https://github.com/user-attachments/assets/0e8c4f96-5bb6-434a-9fb8-607c77a73768)

Whenever changes occur, the values are updated, totals are recalculated, and the button states are set based on available functions:

![image](https://github.com/user-attachments/assets/f8b3ebdd-b7c2-41a0-8202-b4a0721bbc0f)
![image](https://github.com/user-attachments/assets/2e05aecc-9b1f-4a7e-8495-0f90486a7bf5)
![image](https://github.com/user-attachments/assets/112e5aea-cc3d-4385-a92d-9fda64d96a93)

When items are purchased, all values are reset, and the `SceneInitializer` script is called to handle actions related to placing animals:

![image](https://github.com/user-attachments/assets/6530e2f2-4d9f-41cb-a39c-7263cd29964a)

The `SceneInitializer` has two methods it calls during execution:

![image](https://github.com/user-attachments/assets/f0ab5d42-b6b1-4a38-960b-130d54f2d58c)
![image](https://github.com/user-attachments/assets/c339c29e-ed98-4e49-99ba-83fa1310a29c)

`SceneInitializer` includes an integer variable that holds the number of animals to be placed, indicating whether the player has purchased any new animals:

![image](https://github.com/user-attachments/assets/3b092836-ba50-437b-9010-6229888f4996)

The `Update` method checks if there are new animals to be placed. If so, it enters placing mode. Afterward, the basket from the shop is cleared, and the canvas is switched back to the main view:

![image](https://github.com/user-attachments/assets/305333e5-60fc-464a-ba6c-a04ae72d848b)

`PlaceBasketItem` takes each item from the basket and passes it to the `PlaceObject` method:

![image](https://github.com/user-attachments/assets/9ab23ce4-595b-4fda-9615-2e379b4e2926)

The `setAnimalsToBePlaced` method is used by the `ShopManager` to update the number of animals whenever a purchase is made:

![image](https://github.com/user-attachments/assets/ca65803b-3e67-4d7f-9788-27c0c440f04e)

---

We decided to use different canvases for each view instead of separate scenes to make it easier to control the positioning of animals when returning to the main view. This approach also allowed us to see the animals in the real environment at all times. To manage this, I created the `CanvasManager` script, which activates or deactivates each canvas based on the current game state:

![image](https://github.com/user-attachments/assets/5d3bc585-fe1c-4a92-88dc-a89bd5379a03)

---

### Challenges

It was a little confusing to work with instantiating objects, as this can be done both through code and by dragging and dropping game objects into the script fields. Similarly, triggering methods sometimes left me uncertain about the best approach due to my unfamiliarity with Unity's interface. Another challenge was keeping the code modular when dealing with numerous UI components. My expertise usually lies in backend development, so managing text objects and tracking values was tricky and made it challenging to keep the code clean :)

My last task was to handle placing new animals onto the canvas. This allowed me to engage more deeply with the code my teammates had already implemented and learn how real-time code execution works in the `Update()` method.

Overall, this course has been my favorite this semester so far :) I feel that I’ve learned a lot, and my teammates have been very helpful throughout the process. It was exciting to see our results and enjoy the game myself. I hope that in future VR projects, I’ll have the chance to work more with 3D objects and explore their interactivity.
