Space Invaders by Cameron Micallef

Player settings can be found and edited on the Player object in the scene Heirarchy in the Script component "Player"

Enemy settings can be found on the manager object in the hierarchy in the script component "EnemiesManager"

Data is saved as a json file in the Data folder in the assets. with more time I would have expanded the amount of information stored eg. recorded 
every game played on that machine. At the moment it stores "wins", "losses", "Highscores", "Rounds played". I would have stored this extra information
as a generic list in the GameRecord Class Object.

I started on introducing a new way of shooting the enemies. the longer you hold the fire button the more powerful the shot. I implemented the one hit
one kill for the time being.

With more time I was planning on adding a high score reward. The reward was the chance to get a "nuke" which would automatically win the round the 
player was on. After getting a high score a pop up would appear which would ask you to input a passcode, if you type in the passcode quick enough 
you get access to a nuke.

I also looked at a random name generator which would create names for every enemy you killed to add some extra impact to the gameplay. These names 
would have appeared next to the enemies as they fell to the bottom of the screen.

I am using a screen post effect plugin from the asset store.