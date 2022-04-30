# Graphics_Lab
## Students:

 - Wajeeh Atrash - 322773946
 
 - Saji Assi - 314831207
## The project Directed by - **Dr. Roi poranne**
## Working Progress:
- **Week1**: discussing the game idea,taking the green light for writing a pitch for the game, working on the pitch ,opening a repo for the project.
- **Week2**: learning Unity implementing vasic movment for the character wolking all direction and jumping it took us a wjile to find a way to implement the jumping mechanism that allows the player to jump only when he is on the ground, beside the movement we added the mechanism to switch controll between the character and made the game first person view, we have an issue with it the player doesn't move to the direction he is facing we searched for a solution and tried 2 different ways and didn't worked for us(3 hours)

![](week2.gif)

- **Week3**:

	- fixing the bug from the last week 1 hour.
	- changing the camera position with player changing 0.5 hour.
	- adding a pickup mechansim to hold cubes 1.5 hour.
	- trying to add iteractble text (didnt work) 1 hour.
	- watching unity pro builder tutrials and modeling a basic room 1.5 hour.
	-implementing a presure plate mechansim for door opening 0.5 hour.
	- design a basic demo level to domenstrate the progress and testing the added mechanism 1 hour.
	- doing some search for how to implement grabler hook gun & portal gun 1 hour.
	![](week3.gif)

- **Week4**:

	Made no progress due to other obligations.
- **Week5**:
	- implementing Grappler Gun using spring joint,we used ray cast to detect ifwe had a collision with an object we can grapple to then we added to theplayer a spring joint component to connect it with the grappling point andapplying physics on the player. (2 hours)
	- the result we got wasn't satisfying ,we tried a different approach byapplying an explosive force on the player at the direction he want to go to(didn't go will) we spend more time trying to improve our first approach andunderstanding why we got this result and we found that the main problem waschoosing the right parameters for spring joint thats fit our movementimplementation. (6 hours)
	- **finale result**:
	![](week5.gif)

- **Week6**:

	- Made no progress due to other obligations.


- **Week7**:
	- now the grapple gun can grab the pickupable items(1h).
	- Crosshair added for better aiming (0.5h).
	- added the bones for an interactable system the shows popup messages for the player (2.5h).
	![](Week7.gif)
	![](week7_1.png)
	![](week7_2.png)
  
- **week8**:
	- we wasn't satisfied about the way the grapplegun grap object so we decided to change it, we tried to change the object material first , we tried to reduce the material friction first and after we grabed it we added the friction back didn't the result wasn't that good, so decided to implement an impulce force on the object and we got a good grabing movment for the object but we couldn't stop it immediatly after implying the force(we need to figure a way to do that). (1 hour)
	![](Week8.gif)
	- we found a bug in the project sometimes the grapplegun doesn't hit the pickupable object intead hits the ground behind it.
	![](Week8_bug.gif)
	- fixing a small isuue in the Iteractable system displaying messages while holding the object (0.5 hour)
	- did a research about  portal guns how to implement and how to render what in the other side of the portal, this week  the main work was shooting two portals and align them on the surfaces(the portals are in the scene from the begining and when we want to shoot we just changing there locations)in addition we took care of some edge cases like overhanging and wall intersections while aligning the portals.(6.5h)
	- for next week we want to put more work on alignment,change the portal shape into an ellipse and implement the teleporting mechanism.
	![](week8.png)
	![](Week8_portal.gif)

