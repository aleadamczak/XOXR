### Introduction

Hello,  
I'm Aleksandra, and I will describe my contribution to our VR project, *Mama's Pizzeria*.

The project is based on the concept of the popular children's game *Papa's Pizzeria*. The goal of the game is to assemble and prepare a pizza according to the customer's ticket in the shortest possible time. This includes:  
- Assembly Station  
- Oven Station  
- Packing Station  
- Delivery Station  

---

### Task Description

I was responsible for the Packing Station, where the user can cut the pizza using a pizza cutter. Each ticket includes a request for a specific type of cut, such as vertical, horizontal, and/or diagonal cuts.  

In the original game, *Papa's Pizzeria*, players could place cuts anywhere on the pizza. However, implementing this functionality on a 3D object turned out to be quite complex.  

I explored several approaches to achieve this:  
1. **Texture Modification**: I initially attempted to allow custom cuts by drawing directly on the pizza. This would edit the texture on top, enabling the user to create unique cuts. However, at the time, we were unsure whether the pizza would have a single texture covering its entire surface or consist of multiple 3D objects. This uncertainty prevented further progress with this method.  
2. **EzySlice Library**: I also considered using an external library, *EzySlice*, but encountered difficulties integrating it into our project.  
3. **Blender-Split Model**: Ultimately, I decided to pre-split the pizza model in Blender. When the user drags the pizza cutter across the pizza, the distance between slices increases, creating the effect of slicing.  

---

### How It Works

The pizza prefab includes two trigger colliders on opposite sides of the pizza and a center collider common to all cuts. When all three colliders are triggered by the pizza cutter, the cut is made.  

#### Structure of Cuts

Each type of cut (vertical, horizontal, diagonal) is a separate GameObject with two child objects representing the edge colliders:  

![Unity Example](https://github.com/user-attachments/assets/390d2feb-ffae-4a36-a296-9512c2b17223)

The **PizzaCutter** script is assigned to the parent object of each cut. This script requires:  
- The type of cut (vertical, horizontal, diagonal).  
- A reference to the Pizza object.  
- References to the edge and center colliders.  
- A vector representing how slices will move when the cut is applied.  
- References to the pizza slices that will move.  

![PizzaCutter Script](https://github.com/user-attachments/assets/702ebc49-5ca4-4f8e-b26c-035adb21af51)

The **PizzaCutter** script listens to events in the Cut scripts. These events are invoked when the pizza cutter triggers the colliders.

---

### Logic Behind Cuts

The **PizzaCutter** script uses an enum to track which colliders have been triggered:  

![Enum Example](https://github.com/user-attachments/assets/2375b949-93c9-4423-8a4f-1747e7e8e21e)

1. When an edge collider is triggered, the state changes from *None* to *First Edge*, allowing the player to start the cut from any side.  
2. After the center collider is triggered, the script waits for the opposite edge to be triggered.  
3. Once all colliders are triggered, the cut is applied, and the specified pizza slices move according to the assigned vector value. The **AddPizzaCut** method is called to update the Pizza object with the completed cut.

---

### Pizza Logic Organization

To maintain clean code, we split the pizza logic into two scripts:  
1. **Pizza**: Handles interaction logic and updates based on player actions.  
2. **PizzaObject**: Stores the state of the pizza, including cuts, for later comparison with the customer's order.

![PizzaObject Script](https://github.com/user-attachments/assets/ebdbe234-33fe-450e-816b-682884a20c9e)  
![Pizza Script](https://github.com/user-attachments/assets/d3d3fa59-6293-4447-b197-ccfe004a3f89)

---

### Improved Implementation

Initially, the logic for cuts was handled entirely by the pizza cutter script. It would identify colliders based on their tags (e.g., *Vertical1*) and apply the cut. While functional, this approach centralized all actions in one script, making it overly complex. Additionally, the pizza cutter prefab required a reference to the pizza prefab it was cutting, which caused issues—Unity does not allow one prefab to reference another.  

To resolve this, we reversed the roles:  
- The pizza prefab now recognizes collisions with the pizza cutter based on its tag.  
- The pizza prefab applies changes to itself, simplifying the structure and making the code more modular.  

---

This is how the cutting process looks in play mode:  

![Pizza Cutting GIF](https://github.com/user-attachments/assets/79aeb16c-5f00-4bc0-b06b-1d6a8985ab57)

The cuts also have an audio assigned to make the action more satisfying :)

### Conclusions

As much as this task didn't seem very complicated at first, it became trickier during implementation. It gave me a lot of room to familiarize myself with Unity and Blender. Game objects' physics components, like Rigidbody and colliders, turned out to be especially important, as the wrong setup caused many bugs.

Initially, the pizza prefab was set to rotate to a specific degree when it reached the packing station, making it easier for the user to place the cuts in the correct directions. However, modifying the Rigidbody component caused the pizza to fall through the table :)

Another aspect that still needs adjustment is the slice direction of movement when being sliced. Right now, the pizza takes on an unnatural shape after cutting. However, after applying the same vector changes to slices in the prefab, it behaves as expected.

Thank you for reading, and I hope you got the chance to enjoy the game :)
