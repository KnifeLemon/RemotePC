# RemotePC
Hole Punching 기술을 이용한 원격제어 프로그램

### 공부용이었으며 모든 기능이 구현이 안돼있고, 코드가 불안정할 수 있습니다.

![캡처](https://user-images.githubusercontent.com/13937016/131717156-93c1c8cc-8f22-435c-9a6b-255882ef290d.jpg)

## - 사용시 변경할 것 -

[[RemotePC_/SocketUtils/Client.cs 의 21번 줄](https://github.com/KnifeLemon/RemotePC/blob/08e2e5cde2473952a09a787badd753b5d9082360/RemotePC_/SocketUtils/Client.cs#L21)]
> public IPEndPoint ServerEndpoint = new IPEndPoint(IPAddress.Parse("192.168.0.7"), 27000);

의 아이피와 포트 (펀치서버의 외부 아이피를 작성해주세요.)


[[CenterServer/Program.cs 의 15번 줄](https://github.com/KnifeLemon/RemotePC/blob/08e2e5cde2473952a09a787badd753b5d9082360/CenterServer/Program.cs#L15)]
> static int Port = 27000;

의 포트 (펀치서버는 해당 포트가 포트포워딩 돼 있어야 외부연결이 가능합니다.)

를 맞춰주시길 바랍니다.


## - 기능 -


1. UDP Hole Punching 기술 [[기술 설명](https://cjwoov.tistory.com/5)]
2. UDP 통신이 힘들 경우 TCP 통신으로 이용
3. 사용자 별 고정 ID (팀뷰어와 비슷하다 생각하시면 됩니다.)
4. UDP에서의 이미지 분할전송

## - 미구현 기능 -

1. 마우스 이동, 클릭 (이동 구현은 되어있으나 제대로 테스트를 안해봤습니다.)
2. 키보드 전송 (테스트 안해봤습니다.)

## - 추가 정보 -

1. 원격제어시 이미지 퀄리티를 높이고 싶을 경우
[[RemotePC_/MainForm.cs 의 337번 줄](https://github.com/KnifeLemon/RemotePC/blob/08e2e5cde2473952a09a787badd753b5d9082360/RemotePC_/MainForm.cs#L337)]
> img = VaryQualityLevel(img, 50L);

를 수정하면 됩니다.
(이미지의 퀄리티를 낮추면 이미지의 용량이 줄어 전송속도가 높아집니다. 퀄리티가 높을경우 그 반대입니다.)
