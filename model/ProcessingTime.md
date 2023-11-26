# 処理時間
<br>
()内の数値は、初回実行時の処理時間  
単位はms

## Caffe SSD

|model|サイズ|Corei3<br>8130U<br>CPU|Corei3<br>8130U<br>CPU<br>openvino|Corei3<br>8130U<br>iGPU<br>OpenCL|Corei3<br>8130U<br>iGPU<br>openvino|Ryzen5<br>4500U<br>CPU|Ryzen5<br>4500U<br>CPU<br>openvino|Ryzen5<br>4500U<br>OpenCL|Corei5<br>3337U<br>CPU|Corei5<br>3337U<br>openvino|
|:-:|:-:|--:|--:|--:|--:|--:|--:|--:|--:|--:|
|SSD300x300|300|  506<br>(990)|  498<br>(1,327)|  663<br>(4,126)|    143<br>(819)|  280<br>475|   180<br>(376)|   918<br>(2,654)|862<br>(1,607)|---|
|SSD512x512|512|1,270<br>(1,557)|1,529<br>(2,488)|1,982<br>(21,991)|    374<br>(10,079)|  778<br>(1,057)|   459<br>(835)| 2,718<br>(10,322)|2,210<br>(2,360)|---|

<br>
## YoLov3
|model|サイズ|Corei3<br>8130U<br>CPU|Corei3<br>8130U<br>CPU<br>openvino|Corei3<br>8130U<br>iGPU<br>OpenCL|Corei3<br>8130U<br>iGPU<br>openvino|Ryzen5<br>4500U<br>CPU|Ryzen5<br>4500U<br>CPU<br>openvino|Ryzen5<br>4500U<br>OpenCL|Corei5<br>3337U<br>CPU|Corei5<br>3337U<br>openvino|
|:-:|:-:|--:|--:|--:|--:|--:|--:|--:|--:|--:|
|YoLov3 S|320|317<br>(912)|  309<br>(1,069)|  344<br>(2,493)|   93<br>(1,950)|
|YoLov3 M|416|497<br>(1,387)|519<br>(1,346)|635<br>(7,324)|138<br>(15,053)|
|YoLov3 L|608|1,056<br>(1,931)|1,125<br>(1,965)|1,482<br>(2,716)|339<br>(2,259)|
|YoLov3 spp|608|1,026<br>(1,969)|1,149<br>(1,928)|1,530<br>(24,589)|340<br>(2,426)
|YoLov3 tiny|416|  58<br>(224)|  51<br>(286)|    58<br>(13,038)|  25<br>(337)|

<br>
## YoLoX

|model|サイズ|Corei3<br>8130U<br>CPU|Corei3<br>8130U<br>CPU<br>openvino|Corei3<br>8130U<br>iGPU<br>OpenCL|Corei3<br>8130U<br>iGPU<br>openvino|Ryzen5<br>4500U<br>CPU|Ryzen5<br>4500U<br>CPU<br>openvino|Ryzen5<br>4500U<br>OpenCL|Corei5<br>3337U<br>CPU|Corei5<br>3337U<br>openvino|
|:-:|:-:|--:|--:|--:|--:|--:|--:|--:|--:|--:|
|YoLoX s|640|  363<br>(452)|  247<br>(547)|   233<br>(35,291)|    96<br>(12,581)|   215<br>(271)|   113<br>(405)|   970<br>(1,885)|   363<br>(452)|   ---|
|YoLoX m|640|  779<br>(975)|  662<br>(1,059)|   608<br>(2,867)|   203<br>(13,869)|   428<br>(567)|   210<br>(563)| 2,156<br>(8,675)|   779<br>(975)|   ---|
|YoLoX l|640|
|YoLoX x|640|

