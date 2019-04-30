# freebird-vr
A forest-themed VR demo, for GOmmunity's April 2019 developer challenge (https://discord.gg/jeJscNh). Currently built for: Oculus Go and GearVR.

[flying gfy]()

The focus of the demo is to have a visually appealing environment, with a sufficient number of trees to require some form of rendering optimization. For me, a bonus would be a pleasing experience in being able to fly through the environment. A strong sense of "presence" was a given.

TBH, the end result cannot strictly be called a "forest", but is rather a section of a forest, centered around a river. It contains 48 trees, and the overall poly count is: 14K trianges, 39.6K vertices, 8 drawcalls. This polycount would double (in the worst case), being used once per eye.

# Key files:
1. To try it out, sideload this APK to your Oculus Go/GearVR using `adb install Freebird-Oculus.apk`: [file]()
2. The Unity Project is: [Freebird-Oculus]()
3. The Blender Project file is: [two.blend]()
4. The Gimp file for the terrain is: [Ground.xcf]()
5. The script for flying is: [FlightController.cs]()

# Design plan
The plan was to stay under 25 drawcalls, 25K triangles and 25K vertices. This arbitrarily agressive number is roughly what I've experienced to be the safe budget assured to have smooth performance on any device (including Cardboard). On devices without single-pass rendering, this number will double (i.e. once per eye), and a budget of 50-50-50 is conventional wisdom for mobile VR.

Given that polygon budget, and the requirement to have lots of trees, I decided to be lazy and use a low-poly style that's very common (for good reason) in VR. There's no rule against careful use of Transparency shaders, but I chose not to have any transparent stuff (e.g. for leaves) in order to maximize the number of trees I could place within the rendering budget.

The main "rendering optimization" in this demo is to use texture baking, and to use Mobile/Unlit shader to draw everything. So basically nothing gets lit by Unity, not even the paper aeroplane. There's nothing special about this, it's the oldest optimization in the book :)

A style of blending a texture with a low-poly terrain was used, inspired from Google Daydream Home.

For movement through the scene, I chose to let the user fly! :) This felt awesome, and was partly a way to push myself to get started on that VR flying game I've been meaning to make for so long..

# Implementation
First step was to sketch out a rough layout of the scene. This was done using Unity, and cubes were used to mark out the river and pond.

To get started, I used a lowpoly tree I'd made for a previous project, and placed it everywhere a future tree would be. At this point, it looked like this (4 days till submission):

[image1]()


The second step was to create a lowpoly terrain, and apply a basic texture. The ground+river was simply a single colored texture, and while the river looks a bit "dead" by not moving, it simplified things a lot. A 25x25 Plane mesh was created in Blender, and deformed by manually moving vertices using the "Proportional Editing" mode (shortcut 'O') in "random" mode.

The grass and river texture was manually airbrushed in Gimp to add darker patches where the river and grass met, to add yellow patches along the river bank, and to give the river a flowing look. This resulted in (4 days till submission):

[image2]()


The third step was to create different trees. To save time, I used the same "trunk" model in all the trees, and created a few different leaf sections in Blender. First I'd prototyped some tree designs using cubes in Unity. After 5 varities of trees models were created in Blender, they were randomly resized and rotated by hand in Unity.

Then I used a mesh combine script to combine them into two separate meshes: one mesh for all leaves, and one mesh for all trunks. A green texture was applied to the combined leaves mesh, and a brown one for the trunks mesh. This resulted in (3 days till submission):

[image3]()


The fourth step was to bake all this in Blender. For that, I exported the leaves and trunks meshes using an OBJ exporter script, imported to Blender, and baked everything using Blender's Cycles renderer. A couple of gotchas: the imported model required me to use "Remove Duplicates" and recalculate normals again. The baking settings are in the blender file, with some configurations for PBR.

I quickly tested the game on a Cardboard, to ensure it ran smoothly even there. This resulted in (2 days till submission):

[image4]()


The last bit was to just add in a simple script to allow flying, which I borrowed from an older experiment of mine. It's released in this project under MIT license, so if anyone is interested in making flying games with it, I think that would be a GoodThing(tm) overall for VR.

A quick paper-airplane model was made in Blender, and baked as well. A simple menu to manage some options, and a nice soundtrack from the excellent [TeknoAXE Royalty Free Music channel](https://www.youtube.com/channel/UCtgf00GvfFQVsYBA7V7RwUw), and that's pretty much it!

[image5]()
