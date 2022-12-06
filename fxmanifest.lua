fx_version "cerulean"
game { "gta5" }

author 'Big Daddy'
description 'Character Manager and Interface with fivem-appearance editor'
version '2.0.0'

client_scripts {
	'BigDaddy-CharacterManager.Client.net.dll',
	'Newtonsoft.Json.dll',
	'MenuAPI.dll',
	'BigDaddy-CharacterManager-Models.dll',
}

server_scripts {
	'BigDaddy-CharacterManager.Server.net.dll',
	'Newtonsoft.Json.dll',
	'MySql.Data.dll',
	'BigDaddy-CharacterManager-Models.dll',
}

