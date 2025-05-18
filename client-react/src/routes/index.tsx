import { Button } from "@/components/ui/button";
import { createFileRoute } from "@tanstack/react-router";
import { useTest } from "./-api/use-test";

export const Route = createFileRoute("/")({
  component: HomeComponent,
});

function HomeComponent() {
  const { runTest } = useTest();

  const onClick = async () => {
    const data = await runTest({ text: "Hello" });
    console.log(data);
  };

  return (
    <div className="container mx-auto">
      <p>Home</p>
      <Button onClick={onClick}>Test</Button>
    </div>
  );
}
