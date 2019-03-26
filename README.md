# MSSQL Maintance Job
**코딩 교육을 위한 화면 캡쳐 프로그램**

## 무엇인가?
+ 컴퓨터의 스크린 샷을 일정 주기로 녹화하고, 이를 원격 컴퓨터에서 실시간으로 보는 프로그램 

## 왜 사용하나?
+ 코딩 교육 등 강사의 컴퓨터 화면을 보며 교육을 받을 때, 방금 막 지나간 화면을 놓쳐 버린 경우, 강사에게 요청하지 않고도 실시간으로 지나간 화면을 바로 확인할 수 있음
+ 강의 후에도 강사 컴퓨터의 녹화된 화면을 백업해서 다시 볼 수 있음 

## 어떤 장점이 있나? (동영상 녹화에 비해)
+ 강의 중에도 방금 지난간 장면을 바로 확인할 수 있음 
+ 녹화된 컨텐츠의 용량이 작음
    + 1시간 기준 비디오 (대략) : 1GB
    + 1시간 기준 이미지 (대략) : 100MB
+ 녹화를 위한 별도의 프로그램 설치가 필요 없고, PC의 하드웨어 자원을 적게 차지함
 
## 단점은? (동영상 녹화에 비해)
+ 음성 지원이 불가
+ 초 단위의 단절된 이미지

## 어떤 구조로 동작하나?
![저장폴더](https://github.com/cplkimth/MahlerNo2/blob/master/Document/pics/%EC%A0%80%EC%9E%A5%ED%8F%B4%EB%8D%94.png?raw=true)

(이하 녹화를 하는 PC를 *강사컴*, 녹화된 화면을 보는 PC를 *수강생컴*이라 지칭)

### Recorder
강사컴 화면의 스크린샷을 일정주기로 저장하는 프로그램

### Viewer
수강생컴에서 실행되어 강사컴의 녹화된 스크린샷을 보는 프로그램

### API Server
강사컴에서 수강생컴으로 이미지를 전송하는 API를 제공하는 웹 서버  

## 사용방법은?
### 실행 준비
+ .Net Framework 4.6.1 혹은 그 이상의 버전을 설치한다.
    + https://www.microsoft.com/ko-kr/download/details.aspx?id=49981
+ 실행파일을 다운로드 받고 압축을 푼 후 실행한다.
    + https://github.com/cplkimth/MahlerNo2/blob/master/Document/deployment/MahlerNo2.zip?raw=true


### I. API Server
#### 1. 강사컴에서 API Server를 실행한다.
```
MahlerNo2.Api.exe
```
#### 2. 수강생 컴과 통신할 IP 주소와 포트, 스크린샷 저장 폴더를 지정한다.
![저장폴더](https://github.com/cplkimth/MahlerNo2/blob/master/Document/pics/API.png?raw=true)

#### 3. 관리자 권한으로 아래 명령을 수행한다.
+ netsh http add urlacl http://{IP}:{포트번호}/ user=EVERYONE
+ ex) netsh http add urlacl http://10.10.14.75:3512/ user=EVERYONE

### II. Recorder
#### 1. 강사컴에서 Recorder를 실행한다.
```
MahlerNo2.Recorder.exe
```
![저장폴더](https://github.com/cplkimth/MahlerNo2/blob/master/Document/pics/Recorder.png?raw=true)

#### 2. (필요시) 환경 설정 버튼을 눌러 실행 설정을 변경한다.
![저장폴더](https://github.com/cplkimth/MahlerNo2/blob/master/Document/pics/RecorderOption.png?raw=true)
+ 주기(초) : 스크린샷을 저장할 주기
+ 최근 샷 : 최근 저장된 N장의 스크린 샷 중 동일한 샷이 없을 때에만 스크린샷을 저장 (동일한 스크린 샷이 계속 저장되는 것을 방지)
+ 저장 폴더 : 스크린샷이 저장될 폴더

#### 3. 플레이 버튼을 클릭하여 녹화를 시작한다. 클릭할 때 마다 녹화/일시정지 상태가 변경됨

### III. Viewer
#### 1. 수강생컴에서 Viewer를 실행한다.
```
MahlerNo2.Viewer.exe
```

#### 2. 온라인 혹은 오프라인 모드를 선택하고 접속한다.
##### 온라인 모드
![저장폴더](https://github.com/cplkimth/MahlerNo2/blob/master/Document/pics/Online.png?raw=true)

+ 강사컴에 연결된 상태에서 실시간으로 스크린샷을 보는 경우
+ 강사컴의 IP 주소와 포트 번호를 입력하고 연결

##### 오프라인 모드
![저장폴더](https://github.com/cplkimth/MahlerNo2/blob/master/Document/pics/Offline.png?raw=true)

+ 강사컴에 연결되지 않은 상태에서 수강생컴에 저장된 스크린샷을 보는 경우
+ 백업된 폴더를 지정하고 연결
+ 오프라인 모드를 사용하기 위해서는 먼저 온라인 모드에서 백업을 해두어야 함. 백업에 관해서는 후술.
    + 교육 내용을 집에서 복습하는 경우에 유용
     
#### 3. 스크린샷을 볼 날짜를 더블 클릭한다.
![저장폴더](https://github.com/cplkimth/MahlerNo2/blob/master/Document/pics/Connected.png?raw=true)

+ 스크린샷은 일 단위로 저장됨

#### 4. 패널 사용
##### 온라인 모드 표시
![저장폴더](https://github.com/cplkimth/MahlerNo2/blob/master/Document/pics/On.png?raw=true)

##### 오프라인 모드 표시
![저장폴더](https://github.com/cplkimth/MahlerNo2/blob/master/Document/pics/Off.png?raw=true)

##### 백업
![저장폴더](https://github.com/cplkimth/MahlerNo2/blob/master/Document/pics/Backup.png?raw=true)
+ 해당일의 스크린샷 전체를 수강생컴으로 다운로드하는 백업 창 열기
+ ** 온라인 모드일 때만 표시**

##### 이동
![저장폴더](https://github.com/cplkimth/MahlerNo2/blob/master/Document/pics/%EC%9D%B4%EB%8F%99.png?raw=true)
+ 스크린샷이 찍힌 강사컴의 시각 표시
+ 스크린샷을 이동하는 단축키 
+ 좌/우 화살표는 초 단위 이동
+ 상/하 화살표는 분 단위 이동
+ Control 키와 함께 누르면 5 단위만큼 이동
+ Control + Shift 키와 함께 누르면 10 단위만큼 이동

##### 투명
![저장폴더](https://github.com/cplkimth/MahlerNo2/blob/master/Document/pics/%ED%88%AC%EB%AA%85.png?raw=true)
+ 코딩을 하며 동시에 뷰어를 보는 경우, 뷰어를 투명하게 만들 수 있음

##### 이미지 비율
![저장폴더](https://github.com/cplkimth/MahlerNo2/blob/master/Document/pics/%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EB%B9%84%EC%9C%A8.png?raw=true)
+ 강사컴의 해상도와 수강생컴의 해상도가 다른 경우 이미지의 확대/축소가 일어나는데, 이때 원래 이미지의 가로/세율 비율을 유지할지 말지를 선택

##### 보기/감추기
![저장폴더](https://github.com/cplkimth/MahlerNo2/blob/master/Document/pics/%EB%B3%B4%EA%B8%B0_%EA%B0%90%EC%B6%94%EA%B8%B0.png?raw=true)
+ 패널 : 패널의 표시 여부를 변경
+ 전체화면 : 뷰어의 타이틀 바를 표시 여부를 변경 
** 온라인 모드일 때만 표시**

#### 5. 백업
![저장폴더](https://github.com/cplkimth/MahlerNo2/blob/master/Document/pics/backup_progress.png?raw=true)
+ 백업 버튼을 클릭하면 해당일의 모든 스크린 샷이 수강생 컴으로 전송
+ 백업된 파일이 있으면 강사컴에 연결되지 않은 상태에서도 스크린샷을 볼 수 있음 (오프라인 모드) 
