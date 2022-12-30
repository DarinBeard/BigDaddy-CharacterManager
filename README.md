# Character Manager

## A character manager for creating, editing, cloning, saving, deleting, renaming your characters.
# 

NOTE: THIS REPO IS SLIGHTLY OUT OF DATE AND BEHIND DEVELOPMENT... Please go to https://bigdaddyscripts.com/Products/View/1624/Character-Manager to get the most updated version and find the documentation.

BROUGHT TO YOU BY
# **BIG DADDY SCRIPTS**
Many more FiveM scripts available at:
https://BigDaddyScripts.com

YouTube: https://youtube.com/@bigdaddydonzella
# 
If you download from BigDaddyScripts.com there are perks for support and within the discord. Please go there to get the script. 
PLEASE GO HERE TO DOWNLOAD THE MOST RECENT VERSION: https://bigdaddyscripts.com/Products/View/1624/Character-Manager

This is a FiveM character manager for keeping track of and organizing your characters. You can create, edit, save, update, delete, rename, clone and load your characters. This utilizes fivem-appearance character editor for the creating and editing. So there are two separate resources to install on your server for this to work. Keep reading for all the info you need.

**IMPORTANT**: Run the sql.sql script in your FiveM MySql database to create the table. Do not rename the table or the fields or the script will break

Download the latest release and unzip the script folder somewhere in your resources and add <code>start BigDaddy-CharacterManager</code> to your server.cfg

Put the fivem-appearance folder somewhere in your resources and add <code>start fivem-appearance</code> to your server.cfg
You can get fivem-appearance here (my forked version that I know works with this manager) https://github.com/DarinBeard/fivem-appearance
You also need to add the following lines to your server.cfg

<code>
setr fivem-appearance:customization 1

setr fivem-appearance:locale "en"
</code>

The Customization: if set to 0, hair fade could be selected by the player, otherwise it will be automatic. 

The locale: the name of one file inside locales/, default en, choose the locale file for the customization interface.

<code>/manageme</code> will bring up the menu. The rest is pretty easy.

**CLONING vMENU CHARACTERS**
* load your character via vMenu
* <code>/manageme</code> open the menu
* choose CLONE - and fill in the name
* it should open your character in the editor
* simply press SAVE and it will save your character in this manager

> **NOTE**: You cannot go from this character manager back to vMenu, the options just aren't there inside vMenu to make that happen well.

> **ALSO NOTE**: Tattoos placed via vMenu don't save when cloning the character for some reason. The fivem-appearance editor doesn't like something about vMenu's tattoos.

> If you choose a different model than the MP model, your choices for customization are limited. That's due to the game/model.

> There are various camera angles and ways to view your character while you are editing it. Just try out the buttons in the editor to see how they react.

**DEPENDENCIES** (required):
- fivem-appearance

# 
## DEMO https://www.youtube.com/watch?v=t415MK02ZoQ
# 

## LICENSE
See included LICENSE file. 

### **CREDITS**:
The editor (fivem-appearance) was created by pedr0fontoura (https://github.com/pedr0fontoura/fivem-appearance)

This manager has been tested to work with the forked version at https://github.com/DarinBeard/fivem-appearance

