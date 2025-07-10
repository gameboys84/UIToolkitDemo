# RectTransformDemo
# 功能介绍

Toolbar下主要包括基础的UIToolkit功能

| Scene          | 主要功能                                               | 备注               |
| -------------- | ------------------------------------------------------ | ------------------ |
| UIRotatePanel  | Editor扩展栏                                           |                    |
| UITabs         | 用classname动态控制元素的显隐，激活状态                |                    |
| UIAnimation    | 常见的一些UI设置功能，如切换主题，界面动画，数据填充等 | 更详细的介绍见下面 |
| UIToolbarPanel | 测试默认的UI控件，待完善                               |                    |
|                |                                                        |                    |



# 补充说明

## UIAnimation

- 切换主题

  通过PanelSettings 设置 theme style sheet, 而 tss 用于指定使用了哪些储存颜色变量的Style Sheet

- 界面显隐和动画控制

  用classList的添加和删除来控制显隐,  动画的播放由 Transition Animation 来完成

- 动态加载VisualTreeAsset

  实现了用代码动态创建 VisualTreeAsset, 并设置UserData和事件绑定

- 数据填充

  主要填充的数据有文本和图片资源，对于具有文本的组件，如Label和Button，可以直接设置text属性 `lable.text`；所有的VisualElement都带Image, 可以直接设置`image.style.backgroundImage`