# fARm Devblog - Rosa

### Intro
Welcome to my devblog! I'll only go over the parts of the project that I worked on, but my groupmates have done the same so once you read the 3 devblogs you should have a full idea of our whole project and how was it implemented :)

After discussing on a series of ideas, we finally decided that our AR project was going to be a farming simulator, in order to create a fun game that can teach about sustainable farming. This idea was reflected in many of the game design decisions, three examples are that the player can't kill baby animals for their products, that only well fed and hydrated animals are able to have babies, and that the better taken care of the animals are the products they produce will give more money to the player.

The first thing we did was creating a list of features or requirenments for our game and prioritize them. That meant that every week we could go back to the list, see what was done and take new tasks until we had a playable game, maybe not as complex as we hoped in the beggining, but it definetely fulfills the requirement of teaching about sustainable farming and is still fun to play. 

This is the list of the main things I worked on, apart from that I also helped my group mates when they faced a problem they couldn't fix like a problem we had with placing the animals.

### OWNERSHIP LIST:
- Placing first 2 sheeps
- Bars on top of animals
- Tap animal to feed (no food selected yet)
- Hunger/thirst
- Animals dying when they get hungry or thirsty
- Pregnancy and babys from happy animals
- Game over screen
- Game introduction screen


## Placing first 2 sheeps

The first thing I work on, was that when you start you need at least 2 animals, so they can start having babies and you can get money from them. We decided the first animals would be the sheeps. At the begginning our idea was that they would already be there when you start the game, but I quickily realized that wouldn't work for a AR application, since we needed to let the player scan their surroundings and decide where the sheep should actually be. So I created a new script called SceneInitializer and added some script to detect the player touch (or mouse for development purposes) and simply place a sheep. Once that was working I added a counter, which I used to make sure only 2 sheeps were placed. I also had created a animalCounter script with a animalCount static variable, so every time an animal was placed I was keeping track of it there as well.

![image](https://github.com/user-attachments/assets/44b20a76-5ca8-481b-b5bc-9b30276095e3)

This script was changed a lot, and the functionality to placed items from the basket was also placed here, so it now serves more as an "animalPlacer" script than a sceneInitializer.

## Hunger/Thrist + Bars on top of animals + Tap animal to feed + animals dying when they get hungry

Our animals needed to get hungry and thirsty, and the player needed a way of seeing that they needed to give them food and water.
I took the prefabs of the animals that were already added by Laura and modify them to show hunger and thirst. In order to do that I created a HungerBar prefab, which is basically a slider with an image in the side of the food you are suppose to give that animal (wheat for cows and sheep, seeds for chicken and carrot for pigs). To that slider I added a script that will allow to set the value, and to the animal I added a "hungerController" script that will get the hungerBar controller from the childrens in the controller, and decrease the value at a steady chosen pace. I also created in that script a function for eating that at the beggining I was calling simply when you touched the animals, and that after all the tools functionality was finished it was changed to add the check for the selected tool, so it was only called when you had the correct food for the animal. This script also made it easy to acces the current hunger value of the animal, which we would need later for the quality of the products and checking if th eanimal was happy.

![ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/3803077c-2d05-4a7d-8b2f-5297b75f2abb)

At this point it was also the first time i tried building to my phone, and I found out that in augmented reality the bars where looking where the animals were looking. We thought it would be better if they always faced the player, so I created a script to copy the camera rotation and ensure that the bars would always follow the camera. It was later also used for the eggs.

![image](https://github.com/user-attachments/assets/2ac08e01-0352-4b5a-93b5-35b25c6e3afe)
![image](https://github.com/user-attachments/assets/10928048-fbfb-47a0-8722-aaf3519120f7)

After I had that working, I reapeated it for the thirst and added it to each of the animals prefabs + the babies.

![image](https://github.com/user-attachments/assets/c82a7613-0d56-4c5d-8464-d5ab801572fa)

Now that the animals were getting hungry, I modified the hunger script so that when their hunger or thirst reach 0 (the bar was empty) they would die. When I coded this, dying only meant that the game object were destroid, but after the animation was addedd whihc made it so much better (see Laura devblog). Every time an animal was dying I was updating the animalCount.

# Pregnancy and babies

One of the main functionality of the game is that the animals can have babies. At the beggining we thought that perhaps it could be that when you feed an animal there is a small possibility that they have a baby. Of course this is not how it happens in real life, so I had the idea that maybe they could have a baby when they collide with another animal of their own specie. We also needed to add a random factor to it, and make sure that only happy animals have babies. I created a babyGenerator that, if the animal is not already pregnant, checks for triggers with other animals of the same specie. Once they collide, I check that both animls have their thirst and hunger bar to more than half, and if thats the case, there is a one in 5 posbility that they will have a baby. Once I tried this, they were having a lot of babies and the screen got crowed fast. I added a maximun number of animals, so once you go over 20 animals they won't have more babies, but you can still buy them from the shop. Even then, the animals were having too many babies, so I added a new feauture: a pregnancy period. I added a new bar on top of the hunger and thirst that will only appear when the animals fell in love and once that bar was empty the baby is born. The pregnancy duration that is set to 100 seconds. When the baby is born the animalCounter is updated.

![image](https://github.com/user-attachments/assets/9fbb628c-0afa-406d-b12c-2f938764a19b)

![pregnancy-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/8c96df6e-a2fa-4c45-a570-c968b022a347)

# Game Over screen

At this point of the game most of the functionality was already implemented, but there was no "end" to the game. We decided that the end of the game would be when you run out of animals and that the score would be the money that you had. I created a game over canvas inspired in minecraft's and I added some code to the animal counter to check if all the animals died. The screen is quite simple, as it only has the score and a respawn button. When you click the respawn button the scene is reloaded and you start the game from the beggining.

![gameover-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/de9cefc0-a0c9-4847-8730-1872ad17cb61)
![image](https://github.com/user-attachments/assets/05b9b3f5-5f93-4d66-bfa4-4a1764ab398d)
![image](https://github.com/user-attachments/assets/06149f7d-bc66-4454-9df9-e95c325be456)

# Game Instructions screen

Finally, the last thing I did was creating a "game instructions" canvas with some info text that will show before the player starts the game. I made sure that the sceneInitializer script was deactivated until you click the "Let's go!" button so you couldn´'t start placing animals by mistake.

![image](https://github.com/user-attachments/assets/be4f88a2-9356-417a-bdcc-431edc12dc44)








