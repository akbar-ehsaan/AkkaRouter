// See https://aka.ms/new-console-template for more information
using Akka.Actor;
using Akka.Routing;
using Akka_Router;

Console.WriteLine("Hello, World!");
ActorSystem system = ActorSystem.Create("my-first-akka");

//var calculatorProps = Props.Create<CalculatorActor>()


var calculatorProps = Props.Create<CalculatorActor>()

                            .WithRouter(new ConsistentHashingPool(4)

                            .WithHashMapping(x =>

                            {

                                if (x is Add)

                                {

                                    return ((Add)x).Term1;

                                }



                                return x;

                            }));//                        .WithRouter(new Akka.Routing.SmallestMailboxPool(4));

var calculatorRef = system.ActorOf(calculatorProps, "calculator");

var result1 = calculatorRef.Ask(new Add(10, 20)).Result as Answer;

var result2 = calculatorRef.Ask(new Add(11, 30)).Result as Answer;

var result3 = calculatorRef.Ask(new Add(12, 40)).Result as Answer;

var result4 = calculatorRef.Ask(new Add(13, 10)).Result as Answer;

var result5 = calculatorRef.Ask(new Add(14, 25)).Result as Answer;

Console.WriteLine($"Result 1 : {result1.Value}");

Console.WriteLine($"Result 2 : {result2.Value}");

Console.WriteLine($"Result 3 : {result3.Value}");

Console.WriteLine($"Result 4 : {result4.Value}");

Console.WriteLine($"Result 5 : {result5.Value}");

Console.Read();

system.Terminate();