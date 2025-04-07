local drawableSprite = require("structs.drawable_sprite")
local utils = require("utils")

local hangingLamp = {}

hangingLamp.name = "fantomHelper/ReversedHangingLamp"
hangingLamp.canResize = { false, true }
hangingLamp.placements = {
  name = "reversed_hanging_lamp",
  data = {
    height = 16
  }
}

return hangingLamp
