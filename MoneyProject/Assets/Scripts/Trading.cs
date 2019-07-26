using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trading : MonoBehaviour
{

    void Start()
    {
        
    }

    //일꾼이 거점에 물건을 판매한 경우
    public Resource sellingResource(Resource workerResource, Resource stationResource, int count)
    {
        if (workerResource.count < count)
        {
            GameManager.getInstance().TrustLevel -= 10;
            return workerResource;
        }
            
        else
        {
            int profit = workerResource.price * count;
            workerResource.count -= count;
            GameManager.getInstance().Money += profit;
            GameManager.getInstance().Profit += profit;

            GameManager.getInstance().TrustLevel += 20;

            stationResource.count += count;
            return workerResource;
        }
    }
    //일꾼이 거점에서 물건을 산 경우
    public Resource purchaseResource(Resource workerResource, Resource stationResource, int count)
    {
        int money = GameManager.getInstance().Money;
        if (stationResource.price * count > money)
            return workerResource;
        else
        {
            int disbursment = stationResource.price * count;
            workerResource.count += count;
            GameManager.getInstance().Money -= disbursment;
            GameManager.getInstance().Disbursment += disbursment;

            return workerResource;
        }
    }
    void Update()
    {
        
    }
}
