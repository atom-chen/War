# War
Unity编写的类似于七日杀的生存游戏。(版本：Unity 2018.4.12f1)

* 01 项目文件初始化.
#
# 背包模块UI
* 02 背包模块之界面UI制作.
* 03 动态生成背包物品槽，加载Json测试数据，填充背包物品槽.

#
# 合成模块UI
* 04 合成模块之界面UI制作，背包界面微调.
* 05 合成面板左侧选项卡与内容区域基本逻辑.
* 06 合成选项卡和内容区域数据加载，以及重复点击事件修复.
* 07 合成图谱UI生成.
* 08 合成图谱数据展现.
* 09 资源加载工具类和Json数据处理工具类编写.
* 10 合成面板合成功能区域UI基础逻辑.
* 11 物品拖拽基础逻辑.
* 12 物品拆分与合并逻辑.
* 13 合成图谱切换逻辑.
* 14 物品合成逻辑.

#
# 工具栏模块UI
* 15 工具栏模块UI制作与基本逻辑.
* 16 工具栏模块按键控制.

#
# 角色动作模块
* 17 角色动作模块与标准资源导入.
* 18 角色动作临时配置与测试.
* 19 角色模型优化，双摄像机配置.
* 20 角色武器切换模拟.

#
# 枪械模块
* 21 主枪械(突击步枪)动画配置.
* 22 主枪械使用DOTween优化开镜动作.
* 23 枪械类定义与代码分层.
* 24 枪械射线射击基础逻辑模拟测试.
* 25 突击步枪射击音效和枪口特效添加.
* 26 射击弹痕融合与消除.
* 27 不同材质弹痕和子弹命中特效.
* 28 对象池临时资源管理.
* 29 枪械类与环境物体交互临时测试.
* 30 枪械类V层框架提取.
* 31 枪械类C层框架提取.
* 32 霰弹枪基本逻辑编写.
* 33 霰弹枪刚体子弹射击逻辑.
* 34 霰弹枪弹痕生成逻辑.
* 35 霰弹枪对象池管理特效、弹壳和弹头资源.
* 36 木弓基础逻辑实现.
* 37 长矛基础逻辑实现.
* 38 弓箭、长矛尾部颤动效果实现.
* 39 子弹、弓箭、长矛公共代码抽取基类.
* 40 枪械武器、投掷武器二级父类提取.
* 41 枪械模块焦点切换.
* 42 背包数据更新武器耐久UI.
* 43 枪械武器通过工具栏按键工厂实例化.
* 44 枪械武器耐久UI更新.
* 45 枪械独立准星控制与项目资源管理.
* 46 枪械角色模型动作与输入控制优化.

#
# 地形模块
* 47 游戏场景地形制作与资源管理.
* 48 游戏地形场景环境物体生成.
* 49 移除草资源，优化树木资源，降低Draw Call.

#
# AI战斗模块
* 50 人形AI和动物AI不规则碰撞器制作与基本动作配置.
* 51 AI角色生成基础逻辑.
* 52 AI巡逻基础导航逻辑.
* 53 AI角色默认、行走、奔跑动画简单FSM配置.
* 54 AI攻击玩家角色状态配置.
* 55 AI角色受攻击和死亡状态.
* 56 AI角色与枪械系统基础交互.
* 57 AI受伤血液飞溅特效.
* 58 AI头部伤害加倍.
* 59 对象池管理特效资源逻辑优化.
* 60 布娃娃系统模拟人形角色AI死亡动画.
* 61 AI模块抽象框架提取.
* 62 玩家生命值、体力值UI以及体力值影响角色速度逻辑.
* 63 AI攻击玩家，玩家生命值削减以及UI展现.
* 64 AI、玩家音频管理以及枪械音频丰富.
* 65 玩家角色死亡重置.

#
# 建造模块
* 66 建造模块环形UI框架制作.
* 67 建造模块环形类别UI生成逻辑.
* 68 建造模块环形UI滚轮切换类别逻辑.
* 69 建造类别下具体材料UI逻辑.
* 70 建造具体材料二级菜单滚轮切换逻辑.
* 71 文件资源结构调整优化，加载建造模型.
* 72 建造模型实例化逻辑测试.
* 73 地基模型实例化与初步吸附逻辑.
* 74 建造模型透明着色器.
* 75 建造模型吸附功能完善.
* 76 建造模块代码分层重构.
* 77 建造材料抽象父类定义.
* 78 建造材料普通门形物体实例化.
* 79 门形墙壁和窗形墙壁实例化，并分割多个碰撞体.
* 80 柱子模型填充墙壁缝隙，优化吸附逻辑.
* 81 台阶模型建造吸附地基.
* 82 建造代码bug修复以及AI穿透bug修复.
* 83 门型物体实例化与开关.
* 84 窗型物体建造实例化.
* 85 屋顶和吊灯建造逻辑以及射线检测修复.