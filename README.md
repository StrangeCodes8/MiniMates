# MiniMates

Cobbled together powershell that uses BlockBench models and Mojang API to create miniture versions of your players using their own Minecraft Skin texture

Only works with Java Skins at this time
Only tested in Java
Currently only adds to resource packs, not capable of createing a new resource pack (coming soon)

## How To
Run the Powershell
Enter requested information into GUI
Path to resource pack - can be an unzipped resource back in your .minecraft folder for quick and easy testing
Username - should be all lower case (should change to lowercase for you))
custom_model_data - the number you will assign it use /give @p minecraft:paper{CustomModelData:Number} to give item to yourself in game
item to be used - this is the item that will be retextured 

## ToDo
- Give ability to create new resource pack if files/folders don't exist
- Look into bedrock support
- Verify all code paths
- Clean up code
