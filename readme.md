# Release步骤

- 1.确保UI中的WebApi地址是网络地址
- 2.确保WebApi中数据文件路径是网络路径
- 3.编译UI和WebApi文件
- 4.上传到对应文件夹（暂时借用其他文件夹的壳）

## 填坑记

- AdminLTE的BootStape库和标准的库是不同的，所以，col这个类是没有的！
- 一个模块中的组件要被其他模块使用，别忘记exports节里加上去
- CommonModule是angular的内置模块
