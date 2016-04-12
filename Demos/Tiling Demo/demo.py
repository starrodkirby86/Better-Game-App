# ----------
# You probably don't need to care about any of this stuff, but it might be useful if you need to see how my demo works inside
# ----------

# The module I used to draw all of this.
import pyglet
# Used for random terrain generation.
import random

# Seeds the RNG with the current system time.
random.seed()

# Width and height, so they can be changed easily later.
windowWidth = 800
windowHeight = 608

# The same for the size of our tiles.
tileSize = 32

# The width and height of our tileset (divided by tileSize).
tileWidth = 10
tileHeight = 4

# The window I draw the tilemap to
game_window = pyglet.window.Window(windowWidth,windowHeight)
# Here I've overloaded the draw-window event to do what I want it to, that is, clear the window then draw my tilemap.
@game_window.event
def on_draw():
	game_window.clear()
	tileTexture.blit(0,0)

# ----------
# You can start caring now
# ----------

# ~~~ Variables start here ~~~

# tileImage loads in the tileset; I then chop it into a grid, which is stored in tileSet.
tileImage = pyglet.resource.image('tilesetSchool.png')
tileSet = pyglet.image.ImageGrid(tileImage,tileHeight,tileWidth,32,32)

# tileTexture is the image where I build and ultimately draw the tileset.
tileTexture = pyglet.image.Texture.create(windowWidth,windowHeight)

# The map of tiles.  0 equals solid, 1 equals nonsolid.
# If you change the numbers around, the map'll look different.  Try it!
tileMap = [[1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,1,0,0,1,1,1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1],
[1,0,1,1,1,0,1,0,1,0,1,1,0,1,1,0,0,0,1,0,0,0,0,0,1],
[1,0,0,1,0,0,1,0,1,0,0,1,0,1,0,0,0,0,1,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,1,0,1,0,0,0,0,1,1,1,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,1,1,0,1,1,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,1,1,1,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,1,0,1,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,1,1,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1]]

# autodrawMap will hold the coordinates I need to draw the map, in [x,y] format.  (So in that sense, it's a three-dimensional map to our two-dimensional map.)
# The current input is just to initialize it.
autodrawMap = [[1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],
[1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1]]

# The tile dictionary holds the x-y coordinates (divided by the tile size) of each solid tile in my tile map, and what arrangement of bordering solid tiles they correspond to.
# 1 means solid, 0 means empty.  Order is North - Northwest - West - Southwest - South - Southeast - East - Northeast.
# You should be able to take the values within and use it in Unity.
tileDictionary = {
1110: (0,0),
111110: (1,0),
111000: (2,0),
1000: (3,0),
11111011: (4,0),
11101111: (5,0),
10111110: (6,0),
11111010: (7,0),
10101000: (8,0),
11101000: (9,0),
10001111: (0,1),
11111111: (1,1),
11111000: (2,1),
10001000: (3,1),
11111110: (4,1),
10111111: (5,1),
10101111: (6,1),
11101011: (7,1),
10001010: (8,1),
10001011: (9,1),
10000011: (0,2),
11100011: (1,2),
11100000: (2,2),
10000000: (3,2),
1010: (4,2),
101000: (5,2),
10101110: (6,2),
10111010: (7,2),
10101010: (8,2),
# (9,2) is an empty space tile
10: (0,3),
100010: (1,3),
100000: (2,3),
0: (3,3),
10000010: (4,3),
10100000: (5,3),
10101110: (6,3),
10101011: (7,3)
# (8,3) is an empty space tile
# (9,3) is an empty space tile
}

# ~~~ Functions start here ~~~

# isSolid determines whether a given set of coordinates on a map is, well, solid.
def isSolid(testedMap,x,y):
	# First, we check if the tile is out of bounds (in which case it's considered solid).
	if (y < 0) or (x < 0) or (y >= len(testedMap)) or (x >= len(testedMap[y])):
		return True
	if testedMap[y][x] > 0:
		return True
	return False

# makeKey generates a key for a tile, allowing the program to find its coordinates in the tileset within the tile dictionary.
# Fun fact: The keys I've generated are all equivalent to the binary representation of unique numbers, I just couldn't be bothered to translate them over and work out the new makeKey.
# (I also thought this way would be more readable.)
def makeKey(testedMap,x,y):
	key = 0
	# What's important to understand about the if tree seen below is that we only care whether a corner is occupied if both the cardinal directions bordering it are also occupied.
	# Otherwise we can ignore that corner and still generate an appropriate graphic, which greatly cuts down on tile graphics needed.
	if isSolid(testedMap,x,y-1): # North
		key += 10000000
		if isSolid(testedMap,x-1,y) and isSolid(testedMap,x-1,y-1): # West and Northwest
			key += 1000000
		if isSolid(testedMap,x+1,y) and isSolid(testedMap,x+1,y-1): # East and Northeast
			key += 1
	if isSolid(testedMap,x,y+1): # South
		key += 1000
		if isSolid(testedMap,x-1,y) and isSolid(testedMap,x-1,y+1): # West and Southwest
			key += 10000
		if isSolid(testedMap,x+1,y) and isSolid(testedMap,x+1,y+1): # East and Southeast
			key += 100
	if isSolid(testedMap,x-1,y): # West
		key += 100000
	if isSolid(testedMap,x+1,y): # East
		key += 10
	return key

# generateAutomap tests each tile in the given map of 0s and 1s (passingMap) for solidity; if it's solid, it finds its tile in the solid-tile dictionary.  Otherwise, it assigns a random empty-space tile.
# ((9,2), (8,3) and (9,3) are all empty-space tiles in my tileset.)
def generateAutomap(passingMap,lexicon,newMap):
	for y in range(0,len(passingMap)):
		for x in range(0,len(passingMap[y])):
			if isSolid(passingMap,x,y):
				newMap[y][x] = lexicon[makeKey(passingMap,x,y)]
			else:
				randomVar = random.random()
				if (randomVar > .05):
					newMap[y][x] = (9,2)
				elif (randomVar > .02):
					newMap[y][x] = (8,3)
				else:
					newMap[y][x] = (9,3)

# blitInMap takes the completed autodrawMap of tile coordinates and, using this information, automatically blits it into our tilemap for us.
def blitInMap(blitMap,blitTarget,tiles):
	# For each y-row in the map:
	for y in range (0,len(blitMap)):
		# For each x-row in the map:
		for x in range (0,len(blitMap[y])):
			# The tile located at (i,j) in the autodraw map contains the coordinates of the tile we need to draw to that location on the tilemap.
			tileTarget = blitMap[y][x]
			# Using div and mod to extract this information from the autodraw map, we draw the appropriate cell from the tileset in at our x and y coordinates.
			# Note that in Pyglet, X and Y are 0 at the * lower-left corner *.  Just use x*tileSize and y*tileSize if it's in the top-left in Unity.
			blitTarget.blit_into(tiles[(tileHeight-1)-tileTarget[1],tileTarget[0]].image_data,(x*tileSize),(windowHeight-tileSize)-(y*tileSize),0)
			# Once we're finished with a cell, the x coordinate is incremented for the next cell

# ~~~ Main loop starts here ~~~

# Equivalent to the main function loop.
if __name__ == '__main__':
	generateAutomap(tileMap,tileDictionary,autodrawMap)
	blitInMap(autodrawMap,tileTexture,tileSet)
	# This just boots up the program.
	pyglet.app.run()