
namespace MtGox.Configuration {
    public interface IMtGoxClientConfigurator {
        void Scheme(string value);
        void Host(string value);
        void Port(int value);
        void Path(string value);
        void Key(string value);
        void Secret(string value);
    }
}
