# fARm Devblog - Laura

### Intro
Hey, this is Laura and this is my devblog for the Project “fARm”:). The game name makes a pun with “AR” and “farm”, which we thought was super amusing.

The appeal of making this game in AR is to enhance the stress of a farm simulation game, because not only do the animals get needy very quickly, the more animals you have, the more you have to run around to tend to all of their needs and collect all of their produce, and having to move and chase them physically adds to the thrill.

We were three people, so during development, we iteratively took ownership of certain features off of our requirement list. From time to time, we would elaborate on top of each other’s code if it seemed fitting (for example, for reusing functionality).

Below are: a list of the features I took ownership of, and a list of features I did not own but expanded upon:

### OWNERSHIP LIST:
- Resource & Animation Management
- Idle Animal Behaviour
- Ledge Detection & Avoidance in Animals
- Animal Interaction Mechanic (Food & Tools)
- Produce Quality based on Animal Satisfaction Calculation
- Produce Acquisition UI Feedback
- Produce Cooldown
    - Chicken Egg Drop & Pick-Up Mechanic
    - Sheep Wool Regrowth
    - Cow Milk Regeneration & Visual Cues
- Baby Growth Functionality
- Audio Management

### HELP LIST:
- Buttons Navigation Rework
- Shop Recoloring for Improved Visual Understanding
- Pregnancy Visual Cue

# OWNED FEATURES

## RESOURCE & ANIMATION MANAGEMENT
### Resource Acquisition - the Animal Models
One of the reasons I brought up the idea of making a Farm Simulator heavily inspired on Minecraft was that I was 99% sure that models for the farm animals could be found online - so I took it upon myself to find them, import them and animate them.

I was fortunate enough to find them rigged - it took some digging online, but then it was as easy as importing their model file and manipulating each “part” of them however I wanted!

<img width="400" alt="Screenshot 2024-10-29 at 11 17 42" src="https://github.com/user-attachments/assets/0e88d650-0c96-43f0-9206-0822a951d7b0">

There were some challenges regarding scaling them, managing their position (especially on the Y axis, because some just wanted to float eternally) and sometimes even their rotation, but I ended up managing to make proper prefabs that could be easily expanded upon.

<img width="400" alt="All animals Prefabs" src="https://github.com/user-attachments/assets/3f62cdab-a3f0-40a2-8a37-1b72bf13cc59">
<img width="200" alt="Chicken" src="https://github.com/user-attachments/assets/f2bb613e-be7a-41d7-8b3e-c44a087c0cd4">

### Animation - Walking
The first animation to make was the Walking animation for when they idle around. This involved finding their front & back /  left & right leg GameObjects and rotating them around the Z axis with a sine function, since it’s an oscillating movement. It worked perfectly!

![ezgif-3-652a49327e](https://github.com/user-attachments/assets/ec5254de-7533-4c0c-8c9a-4156863ef8e8)


I also had to consider that we had 3 four-legged animals and one with just two legs (the chicken), so I actually had to make separate scripts for these two types of walking animations. But it was simple because they were the same at core.

### Animation - Dying
Another animation that was much needed was the dying animation, and as simple as it sounds to make the animal “fall to the side”, it was a nightmare and took way more hours than I would like to admit!

At first, it seemed easy: since the animals had Colliders and Rigidbodies, I could just rotate them, and they’d fall with gravity. But then, I realized that they’d push each other off ledges, and also that the scene could quickly get overcrowded with collisions... So I made the animals Kinematic to prevent that from happening.

Of course, this meant gravity wasn’t on my side anymore… So now, I had to manually rotate each animal and make it fall to the ground level. It sounds straightforward, but getting it to look right took a lot of fine-tuning, not to mention I had to do it for all adult & baby versions of the animals, which come in different sizes.

![ezgif-4-5ecda01f6e](https://github.com/user-attachments/assets/4b1154fc-b712-4248-8f38-a38952aaf8a4)

## IDLE ANIMAL BEHAVIOUR
There’s never much to say about idle behaviour. Set the starting position as the “anchor” and walk to a random position around it with a certain speed. This also involved some state management in order to get the Walk animation to start and stop at the right time, but it was also fine.

## LEDGE DETECTION & AVOIDANCE
### Why?
A much tricky & interesting part of my development was the ledge detection and avoidance from the animals! Especially considering that their “landscape” is ever-changing in an AR environment - the planes are generated as the player walks around and scans the environments, so the animals should know where they can walk or not.

### How?
This involved projecting a raycast facing downwards in front of the animal - if the raycast collided with a plane, the animal was safe to keep walking. But if the raycast suddenly had the rug pulled from under it, the animal would know to walk a little bit backwards to avoid falling or going into a wall.

![ezgif-7-24067c1275](https://github.com/user-attachments/assets/a27bdc08-5c8a-4d45-83d2-f4c9d8c8e21e)

### Challenges?
The most challenging part about this was the state management in conjunction with the Idle Behaviour script and the Death Animation script. Each of these scripts had to timely block each other so that the sheep wouldn’t walk at the same time as it dies, or keep walking to an undesirable location.

### Room for Improvement
In retrospective, my state management might be a little bit “spaghetti”, but given that it was just these three states, I think it’s forgiveable. If the functionality kept scaling up, I would implement a separate script for managing the state of the animal. But it didn’t seem crucial for this project.

## ANIMAL INTERACTION MECHANIC (FOOD & TOOLS)
I also took ownership of the interaction with the animals, meaning - making things happen when you click on one of them, depending on what tool you have selected from the GUI.

It is possible to interact with the animal in the four following ways:
- Give Water the animal to quench its thirst
- Use a Tool on it (such as a Bucket to collect Milk or Shears to collect Wool)
- Use a Sword on it to kill it for meat
- Give it Food to satisfy its hunger

<img width="251" alt="Screenshot 2024-10-29 at 17 41 04" src="https://github.com/user-attachments/assets/8cd66637-3ee4-4bc3-b89a-7351c4f521bd">

Despite there being four types of interactions, I managed them through just two scripts: **Feeding.cs** for Food & Water, and **Produce.cs** for Tools and Slaughter.

### Food & Water
Even though the animals eat different types of food depending on their species, it was easy to centralize all the logic for making sure the player could only feed it with the right food in hand.

In the Scene Inspector, in the Animal prefab’s component “Feeding”, the developer can choose from a dropdown menu that only accepts the Items existing in the game which food the animal can be fed. So, for the Sheep this would be Wheat, for the Pig this would be Carrot, etc.

Below is the dropdown menu in the editor.

<img width="400" alt="381089854-9813218b-2e2c-4832-b094-2727d4435165" src="https://github.com/user-attachments/assets/2a8fa960-7d19-4b1f-be07-14521c44323a">

Developers, please don't choose a Tool as food for the animal! The trouble of separating Tools and Food was greater than just having them all under the Items type.

Then, inside the actual script, it was just a matter of comparing the “desiredFood” and the “selectedFood”, and voilá. If the wrong food was selected, nothing would happen, but if they were a match, the animal would be fed. This simple logic is below.

<img width="346" alt="Screenshot 2024-10-29 at 11 47 39" src="https://github.com/user-attachments/assets/fbe69c5f-04a5-4f0c-8412-d6402d4978c7">

As for giving the animal water, this is always successful since they all need the same water.

### Tools & Slaughter
When it comes to using tools on the animals, a question was posed - despite all of them behaving so differently when it comes to acquiring produce from them, how could we centralize the logic to keep it as simple as possible?

All animals have the same one script - **Produce.cs**, but in the Scene Inspector, the developer can choose which “type of produce” an animal would produce from each tool: the Sword, the Shears and the Bucket.

The screenshot below is how the Produce component looks like for the Cow - she gives Beef when the Sword is used, nothing when the Shears are used, and Milk when the Bucket is used.

<img width="437" alt="Screenshot 2024-10-29 at 11 48 27" src="https://github.com/user-attachments/assets/228dc8e9-e668-46e9-a0e2-b45ef1d247de">

When it came to the Sword, all the animals had to be assigned their specific meat type (Beef for Cow, Pork for Pig, etc.), so it was kind of “mandatory”, but when it came to the remaining tools, only the Cow had the Milk for the Bucket and the Sheep had the Wool for the Shears. And that was okay! The rest of the animals would have that set as “Nothing” and simply give you nothing if you tried to use the wrong tool on them.


<img width="842" alt="GetProduce Script" src="https://github.com/user-attachments/assets/533e962e-8791-43ea-bfeb-88971376a485">
<img width="583" alt="UseBucket Script" src="https://github.com/user-attachments/assets/ce55f6b0-1077-45df-9366-8919bb2d078d">

The screenshots above illustrate how the Produce script uses the GUI Manager to see what tool is selected, and only returns a piece of produce to the player if that tool is valid on that animal.

## Produce Quality based on Animal Satisfaction Calculation
Well, that’s a long title, but it is what it is - there had to be logic regarding the quality of the produce.

This was as simple as having a switch statement stating how much each type of Produce was worth, and applying some math on it depending on its quality. If the animal was very unhappy, the produce’s value would halve, if it was okay it would be the original value, and if he was very happy, the produce is valued double.

<img width="701" alt="Screenshot 2024-10-28 at 16 54 53" src="https://github.com/user-attachments/assets/97f4e19f-4ef1-4503-82d9-2712e8058dc8">

And how was this “produce quality” calculated? Well, it referenced the Hunger and Thirst controllers of the animal and inquired them as seen below:

<img width="410" alt="int hunger = hungerController hunger;" src="https://github.com/user-attachments/assets/f83987ba-d8f4-4168-9433-25ed831acc72">

If both Hunger & Thirst are above 50%, the quality is 3. If any of them is between 30 and 50, the quality is 2, and if any of them goes below 30%, the quality is 1.

## Produce Acquisition UI Feedback
All of this quality management was happening behind the scenes, but it was hard to tell, as a player, that it was really there. So to really enhance the user experience and transparency, I added some feedback in the UI. When you acquire produce from an animal, you get to see:
- The icon of the produce acquired
- A number of stars representing the quality
- The value in emeralds that it has

And on top of that, it fades in and out beautifully!

This was all achieved by controlling the Canvas with a script ProducePopUp.cs, which has, in the Inspector, an editable list of all the Sprites and which type of produce they are an icon for!

<img width="437" alt="Produce Sprite List for ProducePopUp" src="https://github.com/user-attachments/assets/dff62afe-fbea-4446-9060-bceb51e495da">

When the Script is initialized, it turns this list into a Dictionary for faster lookup when needed, because otherwise, whenever we would want a specific sprite, we would need to iterate through the whole list and that didn’t sound right…

Also, for the case when the player acquires produce faster than it can be displayed with the animations, the produce items acquired are put in a queue and played sequentially! It looks very smooth.

![ezgif-4-a7ae2d4cbe](https://github.com/user-attachments/assets/b887e189-f27f-4a39-b08b-7cbcfffbbf72)

The fading in and out was also a little bit challenging because the Canvas object in Unity doesn’t have a feature to change its transparency in its entirety, so I actually needed a support feature that got all of its children and changed their alpha (opacity) at the same time. Fortunately, it was possible and looks awesome, as it can be seen in the screen recording above).

The screenshot below is the function that manages changing the opacity of all the child elements of the Produce Pop Up. All these elements are either images or text, hence the double "if". It also has a flag "transition" which dictates whether or not the change in opacity should be smooth :).

<img width="848" alt="Screenshot 2024-10-29 at 11 58 34" src="https://github.com/user-attachments/assets/ac55e404-6886-440f-8db4-7313703ba1cc">


## Produce Cooldown
In fARm, it’s possible to get Eggs from Chickens, Wool from Sheep and Milk from Cows. But there’s a lot of nuance in that - you can’t continuously get all of this produce, the animals need time to regenerate it! It took a script to manage each of these Produce items and I will elaborate a bit below.

### Chicken Egg Drop & Pick-Up Mechanic
The Chicken has a script called EggDropper that does exactly what the name suggests - every x seconds, the Chicken drops an egg. The egg is instantiated behind her and has the quality of her satisfaction when she dropped it. 

![ezgif-3-4232228de2](https://github.com/user-attachments/assets/3d130f2f-4103-4ab4-b67c-65d372d05963)

As for picking up the eggs, the Egg has a script called EggPickup that detects clicks on it and gets destroyed and gives money to the player when that happens.

### Sheep Wool Regrowth
The Sheep has a script that manages the Growth and Removal of her wool. This one is interesting, because it actually holds a reference to the “wool” GameObjects on her — yes, the Sheep model was so wonderfully rigged that it is possible to disable the wool separately!! 

<img width="575" alt="Screenshot 2024-10-29 at 12 08 12" src="https://github.com/user-attachments/assets/bd808102-3ed9-46ac-931f-f22ad0f7e51e">

<img width="332" alt="private void SetFur(bool active)" src="https://github.com/user-attachments/assets/e68b6022-3e55-4a63-a2e1-91952eccc14d">

So when we cut her wool, that’s just what happens: The wool is disabled, a timer starts, and within a minute or so, it regrows - which is observed by simply seeing that the wool reappears.

![ezgif-7-e371106b96](https://github.com/user-attachments/assets/c6395327-6f16-4fa9-b01d-87adb9e097db)

### Cow Milk Regeneration & Visual Cues
The same applies to the cow - she has a MilkManager script that establishes how often the player can Milk the cow. On top of that, in order for the player to know that the Cow is ready to be milked, a ParticleSystem was made that shows “milk buckets” flying in a small radius around her, which serves as a perfect visual cue.

(Show screen recording of cow with milk buckets, ready to be milked)

## Audio Management

Another big task I took was to make the audio throughout most of the game. This included:
- Animals “talk” ildly from time to time
- Animals moan when they got slaughtered
- Sparkle sound when a pregnancy occurs
- “Ka-ching” sound when animals are bought in the shop
- Egg “plop” when dropped by chicken
- Bucket sound when taking Milk from Cow
- Snip snip when taking Wool from Sheep
- Water sound when giving water to animals
- Eating sound when feeding animals

But let’s not forget that I had 4 animals to take care of - so how did I do this in the least amount of components and lines of code possible?

Well, just one Component covered a lot of it!

The AnimalSoundController script is one of the things I am the most proud of in this project because it’s one of the most extendable things I have ever implemented.

It makes use of a single Audio Source component in the animal, and in the Inspector, takes a list or a single Audio Clips for each type of sound, as mentioned above, and as shown in the screenshot below:

<img width="462" alt="public class AnimalSoundController  MonoBehaviour" src="https://github.com/user-attachments/assets/fb4ebc50-bfb1-4fa5-a9cd-c243d419caec">

It is then super easy to add that specific animal’s clips to its Prefab in the Inspector!:

<img width="418" alt="Animal Sound Controller (Script)" src="https://github.com/user-attachments/assets/7c46d226-9e41-41e5-b873-c763cebb43ec">

And if it’s a clip the animal doesn’t need, it is perfectly fine to just leave it empty.

<img width="492" alt="void SetClipAndPlay(AudioClip clip)" src="https://github.com/user-attachments/assets/205669ff-cb7a-4d0f-a418-05978531bd39">

The AnimalSoundController then makes extensive use of these two functions to make the right clip play from the single AudioSource: in the right moment, it interrupts whatever the animal’s Audio Source was playing, sets the clip on it and plays it. If needed, it can also get a random clip if there is more than one available.

The AnimalSoundController is then referenced in the relevant scripts whenever a sound needs to play as feedback of a specific action - so, for example, the DeathManager on the animal would simply call soundController.Die() and the Sound Controller would pick a random “pain clip” from the list and play it upon the animal’s slaughter.

## Baby Growth Functionality
This one is simple enough, but given that the animals are babies either when we breed them or when we buy them, there had to be some sort of mechanic that makes them grow within a certain number of minutes.

The BabyGrowth script starts a timer on the baby’s birth or acquisition and, when the time comes, replaces it with its adult counterpart - so, at the same time, the Baby GameObject is destroyed, and the adult one is instantiated with the same position, rotation, and satisfaction! Meaning that the hunger and thirst are actually transferred to the adult as well.

![ezgif-1-fd615c83bf](https://github.com/user-attachments/assets/19aac1e1-984a-47c0-9c24-562881914a30)

# HELPED FEATURES

## Buttons Navigation Change
In fARm, there are a couple of different tools and foods available, but if we were to show all of them at the same time it would take up a lot of space on the screen, so there had to be some sort of “swapping” mechanic between food items and between tool items. Our first idea was to have it as a long press, and this worked for some time, but as soon as the game started becoming more fast-paced, we realized that the delay in the long press cost the lives of a couple farm animals.

Due to this demand of a faster way to swap between items, I changed the Long Press mechanic to a Swipe one - this way, the player can navigate between items back and forth and not only forward, and can do it much more swiftly. This enhanced the gameplay because it enabled the player to perform much better under pressure instead of getting frustrated over the forced delay of a long press.

This also included adding arrows to the GUI to improve understanding that it is possible to change the item selectable in certain “toolbar squares”.

![ezgif-7-69f4d6f6a5](https://github.com/user-attachments/assets/55099bc4-6023-4181-af04-952f89ce56ce)

## Shop Recoloring
I built on top of Ola’s work and changed some of the spacing and coloring in the shop in order for it to have more contrast and be more readable and understandable at a first glance. For this, I am grateful that the Shop’s Canvas object was super well-organized with a good GameObject “family tree”. It made it a lot easier to make changes to the shop :)).

## Pregnancy Visual Cue
The perfect way to let Minecraft players know that there was a pregnancy functionality was to imitate the way it works in Minecraft - by adding a ParticleSystem full of heart icons around the parents when they became pregnant! This was as simple as assigning the Heart Sprite in the Particle System and then instantiating the Particle System briefly whenever a Pregnancy was started.

![ezgif-3-a6bab43650](https://github.com/user-attachments/assets/73642a81-6c12-448b-bd87-0688afaa7d2f)

# Wrap-up
This project was a pleasure to work on - I am proud of all my scripts and how extendable are for the most part - I learned lots in the GMD course about the basics of Unity, and that REALLY enabled me to perform in this AR project and take it a step further, because now that I know the basics, I can foresee challenges and optimize the code so that it’s performing and extendable! I look forward to the VR project as well to keep the ball rolling :))
