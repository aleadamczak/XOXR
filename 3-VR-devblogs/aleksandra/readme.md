Hello,
I'm Aleksandra and I will describe my contribution to our VR project, Mama's Pizzeria.

The project is oriented around the same concept as the very popular children's game, Papa's Pizzeria. The goal of the game is to assemble and prepare a pizza in the same way as described on the ticket from the customer in the shortest possible time. This includes: 
Assembly station,
Oven Station,
Packing Station and 
Delivery station.

I was responsible for the Packing Station where the user will be able to cut the pizza with the pizza cutter. Every ticket comes with request for a different type of cut. It can include vertical, horizontal and/or diagonal cuts.
In the original game, Papa's Pizzeria, you are able to place cuts anywhere on the pizza, however, this has turned out to be quite complicated to implement on a 3D object. 
I have tried different approaches to implement this functionality. Firstly, I tried applying the cuts through drawing them on the pizza. This would allow the user to make completely custom cuts which would edit the texture on top. However, in our case at the point of implementing this functionality we didn't know yet if we are going to have a texture that would cover the whole pizza of if it will be another 3D object therefore I couldn't pursue it any further. Another option was to use an external library EzySlice however there were problems with incorporating it into our project. Eventually, I decided to split the pizza model in blender and when the user drags the pizza cutter across the pizza, the distance between slices increases, creating effect of slicing the pizza.
colliders etc
How does it work?
The pizza prefab has two colliders(triggers) on the opposite sides of the pizza. There is also a center collider which is common for every cut. When all three colliders have been triggered by the PizzaCutter the cut is made.

![image](https://github.com/user-attachments/assets/6da4c5bf-62a2-43c3-a107-3f273317bca2)
![image](https://github.com/user-attachments/assets/f4c7204b-6bea-40fb-9868-f061bf21638a)

All of the Cuts (vertical, horizontal and diagonal) are split into their own GameObjects with two Cuts as their children, the colliders on both edges:

![Unity_XhPTXlNxY7](https://github.com/user-attachments/assets/390d2feb-ffae-4a36-a296-9512c2b17223)
The PizzaCutter script is assigned to the all of the parent Cuts. It requires an indication of the type of cut, a reference to the object with the Pizza script, references to both of the edge Cuts and the Cut in the center, the vector change for the slices the will need to be moved when applying the cut and the pizza slices that will need to be moved:

![image](https://github.com/user-attachments/assets/702ebc49-5ca4-4f8e-b26c-035adb21af51)

The PizzaCutter script is listening to the events placed in the Cut scripts which are invoked whenever the pizza cutter object triggers the colliders.
PizzaCutter script:

![image](https://github.com/user-attachments/assets/7f8ac632-53d4-4919-8e00-fea5ff731809)

Cut script:

![image](https://github.com/user-attachments/assets/1784d2f0-1c18-4ea1-b856-19e3ed49667a)

The PizzaCutter script has an enum variable indicating which colliders (Cuts) have already been triggered:

![image](https://github.com/user-attachments/assets/2375b949-93c9-4423-8a4f-1747e7e8e21e)

Whenever any of the edge colliders get triggered the state is changed from None to First Edge, therefore, it doesn't matter where the player begins their cut. Once the center collider has been triggered as well, the script is expecting the collision with the other edge.

![image](https://github.com/user-attachments/assets/df346477-f6d5-4c2c-876a-2f3ef75af18f)

Once the pizza cutter object gets to the opposite edge, the cut will be made and the assigned pizza slices will be moved in the direction indicated by the Vector value. The AddPizzaCut method will be called on the Pizza to update the cuts that have been made so far:

![image](https://github.com/user-attachments/assets/3a562d74-c359-4b68-91c5-0e1c9c95bfe6)

We have decided to split the Pizza script into Pizza and PizzaObject to keep the code more organised. The Pizza script is the one distributing information and reacting based on what is happening to the Pizza prefab. The PizzaObject is a place holder of all information about the pizza that will be later on compared to the customer order. Therefore, the AddPizzaCut method will be called and it will forward the information about the cuts to the PizzaObject:

PizzaObject:

![image](https://github.com/user-attachments/assets/ebdbe234-33fe-450e-816b-682884a20c9e)

Pizza:

![image](https://github.com/user-attachments/assets/d3d3fa59-6293-4447-b197-ccfe004a3f89)

This is how the cutting process looks like in the play mode:

![PizzaCuttingGIF](https://github.com/user-attachments/assets/79aeb16c-5f00-4bc0-b06b-1d6a8985ab57)


Before implementig this approach, the logic of this action was assigned to the pizza cutter: it would recognize each collider by its tag (e.g. "Vertical1") and apply the cut. Although the actions were very similar, all of them were held in a single script which made it unnecessarily complex. Additionally, for this to work it was necessary for the pizza cutter prefab to have a reference to the pizza prefab it was cutting which becomes problematic because (as it turns out :) ) you cannot have a reference to another prefab in a prefab. Therefore, the roles were reversed and it is the pizza prefab recognising collision with the pizza cutter by its tag and applying the changes to itself. 

