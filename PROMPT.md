I used **ChatGPT** as an AI helper.

Points where I received help:

1\. Interface Usage 

&nbsp;	I used the variables inside the objects and executed functions without the objects referencing each other.

&nbsp;	Interfaces are used for external systems, not for the object itself.

&nbsp;	The internal state of an object is modified via interface properties.

&nbsp;	GameObject references within an interface are managed using properties.

2\. Raycast and Interaction System

&nbsp;	Collision check with object using Raycast

&nbsp;	Using GetComponentInParent<I\_Interaction>() -> to use interface

&nbsp;	Chain calls resulting in errors without null checks

&nbsp;	Interface-based interaction architecture

3\. Input System

&nbsp;	InputAction started, performed, canceled events

&nbsp;	Distinction between press and hold

&nbsp;	Started + Update or Coroutine approach for hold input



Some questions that I asked to ChatGPT:

* How can I call the function on the object I'm interacting with using an interface when I press the interaction key? 
* How can I pass a reference of the GameObject in the character's hand to the interacting object using an interface? 
* How can I prevent conflicts in the input system when pressing and holding down the key?



I mostly used Unreal Engine, and I designed the system to work the same way as in that project. Although it's a small project, I didn't want objects to reference each other. That's why I used an interface system. A clear design approach allows this system to be built more firmly from the ground up. 

So, from the very beginning, I exchanged ideas with ChatGPT about how such a basic system could be built using an interface.

