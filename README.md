# MiniMates

Small application that uses BlockBench models and Mojang API to create miniture versions of your players using their own Minecraft Skin texture  

Only works with Java Skins at this time  
Only tested in Java  
~~Currently only adds to resource packs, not capable of createing a new resource pack (coming soon)~~  
  
![alt text](https://raw.githubusercontent.com/StrangeCodes8/MiniMates/main/image.png?raw=true)

## How To
Run the Executable
Enter requested information into GUI  
Path to resource pack - can be an unzipped resource pack in your .minecraft folder for quick and easy testing  
Resource Pack Name - give a custom name to the resource pack
Username - should be all lower case (should change to lowercase for you))  
custom_model_data - the number you will assign it use /give @p minecraft:paper{CustomModelData:Number} to give item to yourself in game  
item to be used - this is the item that will be retextured   

## ToDo
- [x] Give ability to create new resource pack if files/folders don't exist 
- [ ] Look into bedrock support
- [x] Verify all code paths
- [x] Clean up code
- [ ] Add support for capes
- [ ] Ability to package completed resource pack into zip file
- [ ] Ability to remove minis
- [ ] Ability to preview minis