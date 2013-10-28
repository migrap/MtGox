MtGox
=====

Uses Reactive Extensions to publish information to subscribers.

Here's a simple example to help get your started:
```c#
static void Main(string[] args) {
    var client = new MtGox.MtGoxClient();
    client.ConnectAsync().Wait();

    client.Ticker(x => x
        .Item("BTC")
        .Currency("USD")
    ).Subscribe(x => Console.WriteLine(x));

    client.Trade(x => x
        .Item("BTC")
    ).Subscribe(x => Console.WriteLine(x));

    client.Depth(x => x
        .Item("BTC")
        .Currency("USD")
    ).Subscribe(x => Console.WriteLine(x));

    Console.ReadLine();
}
```
