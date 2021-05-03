# Anywhere UI (Unity3D)
## 기능
* 프리팹을 기반으로 UI를 카테고리화 하여 분리시켜 사용할 수 있도록 만들어줍니다
* 프리팹이 많아질 경우 Project창에서 일일히 찾기 어려워질 때를 위해 허접한 커스텀 에디터 제공 (Tools/AnywhereUI/Open Finder)

## 사용법
간단한 샘플이 준비되어 있으며 (Assets/Scenes/SampleScene.unity), 해당 씬을 플레이 하면 간단한 UI를 불러옵니다.  
테스트 코드는 Assets/Scripts/Test.cs 에 작성 되어 있습니다.  

실제 사용시 아래와 같이 작성 해주세요.  
```C#
namespace AUI
{
  // 프리팹의 경로를 AUIPath로 지정
  [Internal.AUIPath( MyCustomUI" )]
  public class MyCustomUI : Internal.AUIContainer<MyCustomUI>
  {
    public override MyCustomUI OnOpen()
    {
      return this;
    }
    
    // 꼭 구현할 필요는 없음.
    public override void OnClose()
    {
    }
  }
}

public class OpenAndClose : MonoBehaviour
{
  void SomeMethod()
  {
    // 열기
    AnywhereUI.Open<AUI.MyCustomUI>();
    
    // 닫기
    AnywhereUI.Close<AUI.MyCustomUI>();
  }
}
```

## 주의사항
* Resources.Load를 기반으로 하며, AssetBundle과 AddressableAsset은 지원하지 않습니다.  
  나중에 제가 필요해지게 된다면 수정될듯.
* 새로운 UI 작성시 반드시 지킬 필요는 없으나 AUI 네임스페이스에 넣어주세요.  
자동완성 기능을 이용해 현재 코드로 사용 가능한 UI를 쉽게 보기 위함입니다.
