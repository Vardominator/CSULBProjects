import cocos
from cocos.director import director

class HelloWorld(cocos.layer.Layer):
    def __init__(self):
        super(HelloWorld, self).__init__()

        shift = 120
        # create a label
        # create intersections
        intersections = []
        for x in range(0 + shift,800 + shift, 40):
            for y in range(0 + shift, 800 + shift, 40):
                isection = cocos.sprite.Sprite('samples/ball32.png')
                isection.position = x, y
                self.add(isection)
        # label = cocos.text.Label(
        #     'Hello, world',
        #     font_name='Times New Roman',
        #     font_size=32,
        #     anchor_x='center', anchor_y='center'
        # )
        # label2 = cocos.text.Label(
        #     'Hello, world',
        #     font_name='Times New Roman',
        #     font_size=32,
        #     anchor_x='center', anchor_y='center'
        # )
        # label.position = 320, 240
        # self.add(label)
        # label2.position = 520, 240
        # self.add(label2)


if __name__ == "__main__":
    # initialize and create a window
    director.init(width=1000, height=1000, caption="Lynx", fullscreen=False)

    # create a hello world instance
    hello_layer = HelloWorld()

    # create a scene that contains the HelloWorld layer
    main_scene = cocos.scene.Scene(hello_layer)

    # run the scene
    director.run(main_scene)