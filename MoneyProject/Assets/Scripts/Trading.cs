using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trading
{

    void Start()
    {
        
    }

    //일꾼이 거점에 물건을 판매한 경우, 수익금을 올리고 현재 가진 돈을 올린다. 판매한 물품의 count를 감소시키고 거점에 count를 증가시킨다.
    //잘못된 거래를 요청한 경우 신뢰도가 하락하고 정상적인 거래를 한 경우 신뢰도가 증가한다.
    public Resource sellingResource(Resource workerResource, Resource stationResource, int count)
    {
        if (workerResource.count < count)
        {
            GameManager.getInstance().TrustLevel -= 10;
            return workerResource;
        }
        else
        {
            int profit = stationResource.price * count;
            workerResource.count -= count;
            GameManager.getInstance().Money += profit;
            GameManager.getInstance().Profit += profit;

            GameManager.getInstance().TrustLevel += 20;

            stationResource.count += count;
            return workerResource;
        }
    }
    //일꾼이 거점에서 물건을 산 경우, 지출금 항목 상승시키고 현재 가진 돈을 깎는다. 구매한 물건의 count를 증가시키고 거점의 count를 감소한다.
    //잘못된 거래를 요청한 경우 신뢰도가 하락하고 정상적인 거래를 한 경우 신뢰도가 증가한다.
    public Resource purchaseResource(Resource workerResource, Resource stationResource, int count)
    {
        int money = GameManager.getInstance().Money;
        if ((stationResource.price * count > money) || (stationResource.count < count))
        {
            GameManager.getInstance().TrustLevel -= 10;
            InputManager.getInstance().showMessage("돈이 부족합니다.");
            return workerResource;
        }
        else
        {
            int disbursment = stationResource.price * count;
            workerResource.count += count;
            GameManager.getInstance().Money -= disbursment;
            GameManager.getInstance().Disbursment += disbursment;

            GameManager.getInstance().TrustLevel += 20;

            InputManager.getInstance().showMessage(workerResource.rType + " " + count +  "구매하였습니다.");

            return workerResource;
        }
    }
    //독립자금 전달 시 사용되는 함수, 상해가 아닌 곳에 전달할 경우 일본군의 자금으로 쓰이며 계몽도 하락, 위험도 상승, 자본금 하락이 일어난다.
    //상해에 제대로 전달 시에는 자본금 하락, 계몽도 상승, 위험도 상승이 일어난다.
    public void GiveSubsidy(int subsidyAmount, Station station)
    {
        if(station.gameObject.name != "Sanghai")
        {
            GameManager.getInstance().Money -= subsidyAmount;
            GameManager.getInstance().DangerLevel += (subsidyAmount / 100);
            GameManager.getInstance().EnlightenmentLevel -= (subsidyAmount / 250);
        }
        else
        {
            GameManager.getInstance().Money -= subsidyAmount;
            GameManager.getInstance().EnlightenmentLevel += subsidyAmount/200;
            GameManager.getInstance().DangerLevel += (subsidyAmount / 250);
        }
    }
    //뇌물 전달 시 위험도가 크게 감소한다.
    public void GiveBribe(int bribeAmount)
    {
        GameManager.getInstance().Money -= bribeAmount;
        GameManager.getInstance().DangerLevel -= (bribeAmount / 50);
    }

    public void storeResource(Resource workerResource, Resource stationResource, int amount)
    {
        if(workerResource.count < amount)
        {
            return;
        }
        else
        {
            stationResource.count += amount;
            workerResource.count -= amount;
        }
    }
    
    public void releaseResource(Resource workerResource, Resource stationResource, int amount)
    {
        if(stationResource.count < amount)
        {
            return;
        }
        else
        {
            stationResource.count -= amount;
            workerResource.count += amount;
        }
    }
    void Update()
    {
        
    }
}
