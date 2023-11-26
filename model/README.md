# モデル対応表
MS-COCOのPre-trained modelsを使用。
## CaffeSSD

- 設定

|フレームワーク|推論モデル|
|:-:|:-:|
|Caffe|CaffeSSD|

- Status

|model|サイズ|Corei3<br>8130U<br>CPU|Corei3<br>8130U<br>CPU<br>openvino|Corei3<br>8130U<br>iGPU<br>OpenCL|Corei3<br>8130U<br>iGPU<br>openvino|Ryzen5<br>4500U<br>CPU|Ryzen5<br>4500U<br>CPU<br>openvino|Ryzen5<br>4500U<br>OpenCL|Corei5<br>3337U<br>CPU|Corei5<br>3337U<br>openvino|
|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
|[SSD300x300]|300|◯|◯|◯|◯|◯|◯|◯|◯|☓[*1]|
|[SSD512x512]|512|◯|◯|◯|◯|◯|◯|◯|◯|☓[*1]|

[*1]: 推論結果異常

[SSD300x300]:https://drive.google.com/open?id=0BzKzrI_SkD1_dUY1Ml9GRTFpUWc
[SSD512x512]:https://drive.google.com/open?id=0BzKzrI_SkD1_dlJpZHJzOXd3MTg

<br>
## YoLov3
### Darknet
- 設定

|フレームワーク|推論モデル|
|:-:|:-:|
|Darknet|YoLov3|

- Status

|model|weights|サイズ|Corei3<br>8130U<br>CPU|Corei3<br>8130U<br>CPU<br>openvino|Corei3<br>8130U<br>iGPU<br>OpenCL|Corei3<br>8130U<br>iGPU<br>openvino|Ryzen5<br>4500U<br>CPU|Ryzen5<br>4500U<br>CPU<br>openvino|Ryzen5<br>4500U<br>OpenCL|Corei5<br>3337U<br>CPU|Corei5<br>3337U<br>openvino|
|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
|[YoLov3]|[weight](https://pjreddie.com/media/files/yolov3.weights)|320|◯|◯|◯|◯|◯|◯|◯|◯|◯|
|^|^|416|◯|◯|◯|◯|◯|◯|◯|◯|◯|
|^|^|608|◯|◯|◯|◯|◯|◯|◯|◯|◯|
|[YoLov3-tiny]|[weights](https://pjreddie.com/media/files/yolov3-tiny.weights)|608|◯|◯|◯|◯|◯|◯|◯|◯|◯|
|[YoLov3-spp]|[weights](https://pjreddie.com/media/files/yolov3-spp.weights)|608|◯|◯|◯|◯|◯|◯|◯|◯|◯|
<br>
[YoLov3]:https://github.com/pjreddie/darknet/blob/master/cfg/yolov3.cfg
[YoLov3-tiny]:https://github.com/pjreddie/darknet/blob/master/cfg/yolov3-tiny.cfg
[YoLov3-spp]:https://github.com/pjreddie/darknet/blob/master/cfg/yolov3-spp.cfg

### ultralytics

https://github.com/ultralytics/ultralytics
- 設定

yolo exportを使って、OpenVino形式に変換。

|フレームワーク|推論モデル|
|:-:|:-:|
|OpenVINO|UltralyticsYoLo|

- Status

|model|サイズ|Corei3<br>8130U<br>CPU|Corei3<br>8130U<br>CPU<br>openvino|Corei3<br>8130U<br>iGPU<br>OpenCL|Corei3<br>8130U<br>iGPU<br>openvino|Ryzen5<br>4500U<br>CPU|Ryzen5<br>4500U<br>CPU<br>openvino|Ryzen5<br>4500U<br>OpenCL|Corei5<br>3337U<br>CPU|Corei5<br>3337U<br>openvino|
|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
|yolov3|640|
|yolov3-tiny|416|
|yolov3-spp|416|

<br>
## Yolov4
- 設定

|フレームワーク|推論モデル|
|:-:|:-:|
|Darknet|YoLov3|

- Status

|model|サイズ|Corei3<br>8130U<br>CPU|Corei3<br>8130U<br>CPU<br>openvino|Corei3<br>8130U<br>iGPU<br>OpenCL|Corei3<br>8130U<br>iGPU<br>openvino|Ryzen5<br>4500U<br>CPU|Ryzen5<br>4500U<br>CPU<br>openvino|Ryzen5<br>4500U<br>OpenCL|Corei5<br>3337U<br>CPU|Corei5<br>3337U<br>openvino|
|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|

<br>
## YoLoX
- 設定

|フレームワーク|推論モデル|
|:-:|:-:|
|ONNX|YoLoX|

- Status

|model|サイズ|Corei3<br>8130U<br>CPU|Corei3<br>8130U<br>CPU<br>openvino|Corei3<br>8130U<br>iGPU<br>OpenCL|Corei3<br>8130U<br>iGPU<br>openvino|Ryzen5<br>4500U<br>CPU|Ryzen5<br>4500U<br>CPU<br>openvino|Ryzen5<br>4500U<br>OpenCL|Corei5<br>3337U<br>CPU|Corei5<br>3337U<br>openvino|
|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
|[yolox-s]|640|
|[yolox-m]|640|
|[yolox-l]|640|
|[yolox-x]|640|
|[yolox-Nano]|416|
|[yolox-Tiny]|416|

[yolox-Nano]:https://github.com/Megvii-BaseDetection/YOLOX/releases/download/0.1.1rc0/yolox_nano.onnx
[yolox-Tiny]:https://github.com/Megvii-BaseDetection/YOLOX/releases/download/0.1.1rc0/yolox_tiny.onnx
[yolox-s]:https://github.com/Megvii-BaseDetection/YOLOX/releases/download/0.1.1rc0/yolox_s.onnx
[yolox-m]:https://github.com/Megvii-BaseDetection/YOLOX/releases/download/0.1.1rc0/yolox_m.onnx
[yolox-l]:https://github.com/Megvii-BaseDetection/YOLOX/releases/download/0.1.1rc0/yolox_l.onnx
[yolox-x]:https://github.com/Megvii-BaseDetection/YOLOX/releases/download/0.1.1rc0/yolox_x.onnx
<br>
## YoLov5
### ultralytics/yolov5
https://github.com/ultralytics/yolov5
- 設定

export.pyを使って、OpenVino形式に変換。

|フレームワーク|推論モデル|
|:-:|:-:|
|OpenVINO|YoLo|

- Status

|model|サイズ|Corei3<br>8130U<br>CPU|Corei3<br>8130U<br>CPU<br>openvino|Corei3<br>8130U<br>iGPU<br>OpenCL|Corei3<br>8130U<br>iGPU<br>openvino|Ryzen5<br>4500U<br>CPU|Ryzen5<br>4500U<br>CPU<br>openvino|Ryzen5<br>4500U<br>OpenCL|Corei5<br>3337U<br>CPU|Corei5<br>3337U<br>openvino|
|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
|YOLOv5n|640|
|YOLOv5s|640|
|YOLOv5m|640|
|YOLOv5l|640|
|YOLOv5x|640|

<br>
### ultralytics
https://github.com/ultralytics/ultralytics
- 設定

yolo exportを使って、OpenVino形式に変換。

|フレームワーク|推論モデル|
|:-:|:-:|
|OpenVINO|UltralyticsYoLo|

- Status

|model|サイズ|Corei3<br>8130U<br>CPU|Corei3<br>8130U<br>CPU<br>openvino|Corei3<br>8130U<br>iGPU<br>OpenCL|Corei3<br>8130U<br>iGPU<br>openvino|Ryzen5<br>4500U<br>CPU|Ryzen5<br>4500U<br>CPU<br>openvino|Ryzen5<br>4500U<br>OpenCL|Corei5<br>3337U<br>CPU|Corei5<br>3337U<br>openvino|
|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
|YOLOv5n|640|
|YOLOv5s|640|
|YOLOv5m|640|
|YOLOv5l|640|
|YOLOv5x|640|

<br>
## YoLov8

https://github.com/ultralytics/ultralytics
- 設定

yolo exportを使って、OpenVino形式に変換。

|フレームワーク|推論モデル|
|:-:|:-:|
|OpenVINO|UltralyticsYoLo|

- Status

|model|サイズ|Corei3<br>8130U<br>CPU|Corei3<br>8130U<br>CPU<br>openvino|Corei3<br>8130U<br>iGPU<br>OpenCL|Corei3<br>8130U<br>iGPU<br>openvino|Ryzen5<br>4500U<br>CPU|Ryzen5<br>4500U<br>CPU<br>openvino|Ryzen5<br>4500U<br>OpenCL|Corei5<br>3337U<br>CPU|Corei5<br>3337U<br>openvino|
|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|:-:|
|YOLOv8n|640|
|YOLOv8s|640|
|YOLOv8m|640|
|YOLOv8l|640|
|YOLOv8x|640|
