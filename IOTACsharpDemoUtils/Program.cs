using System;
using Tangle.Net.Cryptography;
using Tangle.Net.Entity;
using Tangle.Net.Repository.Factory;

namespace IOTACsharpDemoUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            SendBalance();
        }

        public static void SendBalance()
        {
            var s1 = Seed.Random();
            var s2 = Seed.Random();

            var repository = IotaRepositoryFactory.Create("https://nodes.devnet.thetangle.org");
            var nodeInfo = repository.GetNodeInfo();

            var mainDevnetSeed = new Seed("PHIDOZJCOJGGYHMP9DPZQGHXYCBMJEJWESNEXGAEUY9OGUOKKUYYEBREJBXRWRSHCWSFPILJBPVCDKGVA");
            //var depositSeed = repository.GetNewAddresses(s1, 0, 1, SecurityLevel.Medium);
            var mainDevnetSeedsenderBalance = repository.GetAccountData(mainDevnetSeed, true, 2, 0, 5);


            //var senderSeed = new Seed("VJEFRYYWLYMKGGQPERQISOAIJRCLKDJKDYFPQYTAEKQVLEFTEYKYKVSEFBWBCLVNBUPNYCUDNXSJQLKWV");
            //var recipientSeed = new Seed("NYLBCMVKSWQJCIVWUSHLOYRAGGJQTMUOUXJKQTT9LYIV9ZWTYANOWRFTUKNVPVEJGHUXNPAPNCSKLFSAC");

            //var senderBalance = repository.GetAccountData(senderSeed, true, 2, 0, 10);
            //var recipientBalance = repository.GetAccountData(recipientSeed, true, 2, 0, 10);

            ////Generate one new recipient address                
            //var recipientAddress = repository.GetNewAddresses(recipientSeed, 0, 1, SecurityLevel.Medium);

            //Prepare transaction
            var transfer1 = new Transfer(
                address: "KVYJYZMDKBHGWRGTZUGDIYDHHVM9EUCPPRHYNTHIVQEZK9XSDRRGGGX9YVOABHQT9CCGNSNUNGEIXXE9XJWDNH9GR9",
                tag: "PATRIQTUTORIALS",
                message: "PatriQ tutorials testing https://patriq.gitbook.io/iota/",
                timestamp: DateTime.Now,
                value: 10);

            //Create a bundle
            var bundle = new Bundle();
            bundle.AddTransfer(transfer1);

            //Send the bundle
            var result = repository.SendTransfer(mainDevnetSeed, bundle, SecurityLevel.Medium, depth: 4, minWeightMagnitude: 9);

            //Check the result in Tangle Explorer
            Console.WriteLine("Link to Tangle Explorer: {0}{1}", "https://devnet.thetangle.org/bundle/", result.Hash);

        }
    }
}
