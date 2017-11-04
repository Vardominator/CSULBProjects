import cocos

# import all available actions into the namespace
from cocos.actions import *

# subclass ColorLayer to have a background color
class HelloWorld(cocos.layer.ColorLayer):
    def __init__(self):
        # blueish color
        super(HelloWorld, self).__init__(64,64,224,255)

        # create a label
        label = cocos.text.Label(
            'Hello, world',
            font_name='Times New Roman',
            font_size=32,
            anchor_x='center', anchor_y='center'
        )
        label.position = 320, 240
        self.add(label)

        sprite = cocos.sprite.Sprite('samples/grossini.png')
        sprite.position = 320,240
        sprite.scale = 3
        self.add(sprite, z=1)

        scale = ScaleBy(3, duration=2)

        # we tell the label to:
        #   1. scale 3 times in 2 seconds
        #   2. then to scale back 3 times in 2 seconds
        #   3. and we repeat these 2 actions forever
        # + operator is the Sequence action
        label.do(Repeat(scale + Reverse(scale)))
        sprite.do(Repeat(Reverse(scale) + scale))


if __name__ == "__main__":
    cocos.director.director.init()
    hello_layer = HelloWorld()

    # tell Layer to execute a RotateBy action of 360 in 10seconds
    hello_layer.do(RotateBy(360, duration=10))

    # a scene that contains the layer hello_layer
    main_scene = cocos.scene.Scene(hello_layer)
    # start the application, starting the main_scene
    cocos.director.director.run(main_scene)