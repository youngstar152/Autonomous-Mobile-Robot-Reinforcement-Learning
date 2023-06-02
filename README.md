# Autonomous-Mobile-Robot-Reinforcement-Learning
Autonomous Mobile Robot with Moving Obstacle Avoidance Based on Simulation and Reinforcement Learning

卒業研究に関するリポジトリです。

本研究はPytorchとUnity、C#で構築されています。

シミュレーション環境の構築、人障害物と移動ロボットの制御はUnity、C#

モデルの作成にPytorch、ML-Agentで.onnxを作成し、Unity上で.onnxを読み込んでいます。

学習の詳細は.yamlに記述しています。

This is a repository about my graduation research.

This research is built with Pytorch, Unity and C#.

Unity and C# for building the simulation environment and controlling the human obstacle and the mobile robot.

Pytorch and ML-Agent are used to create the models, and .onnx are loaded on Unity.

The details of the training are described in .yaml.

# paper_slideフォルダ
卒業論文と発表資料を掲載しています。

Graduation theses and presentation materials are available.

## 本研究で作成した学習済みモデルを使用したシミュレーション

距離センサとグレースケールビジョンセンサを使用し、強化学習によって人障害物を回避するシミュレーション

https://user-images.githubusercontent.com/63835230/234823240-6a7eba3f-3b91-49e6-85bc-2d850185d82d.mp4

