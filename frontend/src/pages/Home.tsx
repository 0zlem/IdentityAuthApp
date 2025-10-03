import GooeyNav from "../components/GooeyNav";

function Home() {
  const items = [
    { label: "Home", href: "/" },
    { label: "Logout", href: "/register" },
  ];

  return (
    <div>
      <div style={{ height: "600px", position: "relative" }}>
        <GooeyNav
          items={items}
          particleCount={15}
          particleDistances={[90, 10]}
          particleR={100}
          initialActiveIndex={0}
          animationTime={600}
          timeVariance={300}
          colors={[1, 2, 3, 1, 2, 3, 1, 4]}
        />
      </div>

      <div></div>
    </div>
  );
}

export default Home;
