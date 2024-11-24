## Intro

Welcome to my devblog! If you have already read the Ar projet one you Will know that I'll only go over the parts of the project that I worked on, but my groupmates have done the same so once you read the 3 devblogs you should have a full idea of our whole project and how was it implemented :)

This time the project is a pizzeria simulator game in VR, where the player can wear the headset and immerse himself in a pizzeria near the beach, attend the customers and cook their pizzas to get the perfect score! It is a really fun game to pplay, and it was even funnier to create!

As well as in the AR Project, we started by drawing a design of how we wanted the game to look and writing down the different tasks in a prioritized list. We knew we wanted different stations, one where you attend the customers, one where you assembly the pizza, one where you cook it (oven), and one where you cut it and put it in a box.

![image](https://github.com/user-attachments/assets/b7562a10-fc47-4601-8ef9-a6a436463a9b)

I mainly focused in the assembly station,a nd once i finished that I started helping with other tasks that were missing.

### OWNERSHIP LIST:
- Assembly station:
  - Creating ingredients and pizza models
  - Kneading pizza dough
  - Putting tomato sauce and cheese
  - Putting ingredients on pizza
- Customers prefabs and fix movement

## Creating ingredients and pizza models

We decided on the following ingredients for the pizza: pepperoni, pepper, olives, fish, mushroom. Luckily, I could find the pepperoni, pepper and fish in the assets store, as well as a roller and bowls to put the ingredients, but I had to create the mushroom, olives, the sauce and the cheese and the actual pizza. I created the mushroom and olive in blender, painted them and imported into unity. The cheese I created directly in unity by convining a bunch of yellow rectangles, and the sauce is simply a bowl painted red and turned around. For the pizza I started with a sphere for the dough and a cylinder for the base. Once all of the models were cretaed I placed them to build the "Asembly station" which is looking like this

<img src="https://github.com/user-attachments/assets/6df25fda-c7c3-419a-b528-e0e5deceee18" width="400"/>
<img src="https://github.com/user-attachments/assets/6c14f3ef-fbb8-41c1-9a67-2260501fff2f" width="300"/>
<img src="https://github.com/user-attachments/assets/01c494a4-3753-4c60-ac3d-472e87095546" width="300"/>
<img src="https://github.com/user-attachments/assets/ed328a7a-bfd0-438e-bac2-30f2d5640cc7" width="300"/>

## Kneding pizza dough

The first thing needed to start the pizza cooking process was kneading the pizza dough.
![image](https://github.com/user-attachments/assets/acc44ba6-f477-4772-8610-46e0b99b43d3) ->
![image](https://github.com/user-attachments/assets/72d91341-2d44-4806-98bf-125de890fee3)

I created the dough ball in blender, but I still don't know how to use it porperly, so I followed the first part of a bouncing ball animation tutorial to get this:

![pizzaknead-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/968cf9bd-8c86-4bba-8015-835bba86fb25)


Once I had that I imported it into unity. I created a prefab with the dough with an animator with the animation, a script to controll it and the pizza base object. In this script I set the base to inactive in the start method, and I listen for collisions with the rolling pin. The start and resume animation functions are simply setting the animation speed to 1 or 0. Once the animation is done, I switch the dough model to the pizza base model, the result looks like this:

![image](https://github.com/user-attachments/assets/cf294213-d67a-4ed9-a4c6-f48cb4642fe5)
![image](https://github.com/user-attachments/assets/1ee53b37-7da7-4527-a1c4-fefd30e742fc)

![este-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/b2c2c86e-fc87-484d-9f36-bb956c29582c)

I wanted to make it look more like kneading, so I though I could add instead of only one collider to detect the rolling pin, a few of them, where only one is active at a time and once you trigger it the next is activated instead. This wuld make the user have to go back and forth in the dough, exactly how you would do if you were kneading the dough, but time was not enough and that idea was left for the future :)

## Putting tomato sauce and cheese

Once we have the rolled pizza, we can add the tomato and cheese. I created different textures in blender (only tomato, only cheese and both).
I took a spoon prefab from the store and added a little bit of sauce to it (red capsule). I created a script to controll it. Basicallly in the start function it deactivates the sauce, and it is only activated again when the sauce bowl triggers it. If the spoon collides with a pizza base, and the sauce is active at that moment, it deactivates the sauce and calls a method in the pizza to add the sauce texture. For the cheese I followed the same approach, but instead of grabbing it with a spoon you take it directly from the bowl, for that I used the ingredient grabber script that I will explain later when I talk about the ingredients. This is the result: 

![image](https://github.com/user-attachments/assets/664c90b8-69a1-4202-a4fe-dbdd2f37ac52)
![sauce-ezgif com-video-to-gif-converter (1)](https://github.com/user-attachments/assets/4972216d-c7ad-4758-bca3-3677028c65d5)
![sauce-ezgif com-video-to-gif-converter (2)](https://github.com/user-attachments/assets/059aadc6-6698-49d9-9294-ef52a3ebdb09)

## Putting ingredients in pizza

The only missing things was putting the ingredients on the pizza. In order to get the score, we were telling the player in which quarter he needs to put what ingredients, so i needed a way to detect that. I added the quarters to the pizza base prefab, which were simply box trigger colliders and a script to detect which ingredient was falling there and adding the to the pizza object to use later for the score.
 In order to get the ingredients, I thought that the best way would be having infinite ingredients, so I creted an Ingredient Grabber script and added them to each of the ingredients bowls. This script is pretty simple: it is a grab Interactable script, which only has one method taht, when called, cancels whatever selection the interaction manager has and selects the ingredient prefab instead, so what the player is doing is grabbing the whole bowl, but they will only see that they grab the actual ingredient. I indicated that this method should be called when selectEnters, and that does the trick, now the player can get as many ingredients as he wants to put them in the pizza (or throw them to the customers)

![image](https://github.com/user-attachments/assets/1cdce788-e981-48f8-8e46-f2197dd19d09)
![image](https://github.com/user-attachments/assets/43ccd375-4602-4f84-b73e-4f07529d809c)
![ingredients-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/84c3d299-8db8-4821-b2a5-36dc95d881ec)

With that, now you are ready to grab the pizza and put it in the oven!!!

## Customer prefabs

By the time I finished with the assembly station, most of the things in our list were done, or being doone by my group mates, but we still had simple capsules as our customers. they were already moving and ordering pizzas, but we needed to make them look good.
Therefore I got some prefabs from the store, that I had previously used for GMD so I new they were supper nice and easy to style and animate. 
I just substitue the existing customers with new ones in the customer generator object, and added all of the scripts that they needed to work, the customer movement and the customer request. I needed to adjust a little bit their movement, since a capsule dont need to be animated, or face where they are moving, but since the customers scripts were so well done it was an easy task for me. This is how they look:

![WhatsAppVideo2024-11-24at10 18 13-ezgif com-crop](https://github.com/user-attachments/assets/9225fa24-8f5f-49c5-bc42-222a2a935da1)

Of course in this devblog you can only see the final solutions, but most of the things I described took a lot of time to figure out and went over a few different ways of implementing until I was happy with them, but finally, I am so happy with how the project turned out, and I hope you also enjoyed it! :)






