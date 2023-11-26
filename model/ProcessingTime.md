# 処理時間
## Caffe SSD

|model|サイズ|回数|Corei3<br>8130U<br>CPU|Corei3<br>8130U<br>CPU<br>openvino|Corei3<br>8130U<br>iGPU<br>OpenCL|Corei3<br>8130U<br>iGPU<br>openvino|Ryzen5<br>4500U<br>CPU|Ryzen5<br>4500U<br>CPU<br>openvino|Ryzen5<br>4500U<br>OpenCL|Corei5<br>3337U<br>CPU|Corei5<br>3337U<br>openvino|
|:-:|:-:|:-:|--:|--:|--:|--:|--:|--:|--:|--:|--:|
|SSD300x300|300|初回|  990|1,327|4,126|    819|◯|◯|◯|1,607|---|
|^|^|       2回目以降|  506|  498|  663|    143|◯|◯|◯|862|---|
|SSD512x512|512|初回|1,557|2,488|21,991|10,079|◯|◯|◯|2,360|---|
|^|^|       2回目以降|1,270|1,529|1,982|    374|◯|◯|◯|2,210|---|

## YoLoX

|model|サイズ|回数|Corei3<br>8130U<br>CPU|Corei3<br>8130U<br>CPU<br>openvino|Corei3<br>8130U<br>iGPU<br>OpenCL|Corei3<br>8130U<br>iGPU<br>openvino|Ryzen5<br>4500U<br>CPU|Ryzen5<br>4500U<br>CPU<br>openvino|Ryzen5<br>4500U<br>OpenCL|Corei5<br>3337U<br>CPU|Corei5<br>3337U<br>openvino|
|:-:|:-:|:-:|--:|--:|--:|--:|--:|--:|--:|--:|--:|
|YoLoX s|640|初回|  452|  547|35,291|12,581|      |      |      |   452|   ---|
|^|^|    2回目以降|  363|  247|   233|    96|      |      |      |   363|   ---|
|YoLoX m|640|初回|  975|1,059| 2,867|13,869|      |      |      |   975|   ---|
|^|^|    2回目以降|  779|  662|   608|   203|      |      |      |   779|   ---|
|YoLoX l|640|初回|
|^|^|    2回目以降|
|YoLoX x|640|初回|
|^|^|    2回目以降|

