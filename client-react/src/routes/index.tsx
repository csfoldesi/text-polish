import { createFileRoute } from "@tanstack/react-router";
import { TextForm } from "./-components/text-form";

export const Route = createFileRoute("/")({
  component: HomeComponent,
});

function HomeComponent() {
  return (
    <div className="container mx-auto">
      <TextForm />
    </div>
  );
}
